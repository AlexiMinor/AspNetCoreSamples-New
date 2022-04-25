using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FirstMvcApp.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IUnitOfWork unitOfWork, 
            IConfiguration configuration, 
            IAccountService accountService, ILogger<TokenService> logger, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _accountService = accountService;
            _logger = logger;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<JwtAuthDto> GetToken(LoginDto request, string ipAddress)
        {
            var user = await _accountService.GetUserByEmailAsync(request.Login);
            if (!await _accountService.CheckPassword(request.Login, request.Password))
            {
                _logger.LogWarning("Trying to get jwt-token with incorrect password");
                return null;
            }

            var jwtToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            refreshToken.UserId = user.Id;
            await _unitOfWork.RefreshTokens.Add(_mapper.Map<RefreshToken>(refreshToken));
            await _unitOfWork.Commit();

            return new JwtAuthDto(user, jwtToken, refreshToken.Token);
        }

        public async Task RevokeToken(string token, string ipAddress)
        {

            var refreshToken = await (await _unitOfWork.RefreshTokens.FindBy(rt => rt.Token.Equals(token)))
                .FirstOrDefaultAsync();

            if (refreshToken == null || !refreshToken.IsActive)
                throw new ArgumentException("Invalid token", "token");

            await RevokeRefreshToken(refreshToken, ipAddress, $"Revoke wthout replacement : {token}");
        }

        public async Task<JwtAuthDto> RefreshToken(string? token, string ipAddress)
        {
            var user = await _accountService.GetUserByRefreshTokenAsync(token);

            var refreshToken = await (await _unitOfWork.RefreshTokens.FindBy(rt => rt.Token.Equals(token)))
                .FirstOrDefaultAsync();


            if (refreshToken == null || !refreshToken.IsActive)
                throw new ArgumentException("Invalid token", "token");

            if (refreshToken.IsRevoked)
            {
                await RevokeDescendantRefreshToken(refreshToken, ipAddress,
                    $"Attempted reuse of revoked ancestor token: {token}");
            }

            var refreshTokenDto = await RotateRefreshToken(refreshToken, ipAddress);
            refreshTokenDto.UserId = user.Id;

            await _unitOfWork.RefreshTokens.Add(_mapper.Map<RefreshToken>(refreshTokenDto));
            await _unitOfWork.Commit();

            await RemoveOldRefreshTokens(user);

            var jwtToken = _jwtService.GenerateJwtToken(user);

            return new JwtAuthDto(user, jwtToken, refreshTokenDto.Token);
    }

        private async Task RevokeDescendantRefreshToken(RefreshToken token, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(token.ReplacedByToken))
            {
                var childToken = await(await _unitOfWork.RefreshTokens.FindBy(rt => rt.ReplacedByToken.Equals(token.ReplacedByToken)))
                    .FirstOrDefaultAsync();
                if (childToken.IsActive)
                {
                    await RevokeRefreshToken(childToken, ipAddress, reason);
                }
                else
                {
                    await RevokeDescendantRefreshToken(childToken, ipAddress, reason);
                }
            }
        }

        private async Task RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            await _unitOfWork.RefreshTokens.PatchAsync(token.Id, new List<PatchModel>
            {
                new PatchModel() { PropertyName = "Revoked", PropertyValue = DateTime.UtcNow },
                new PatchModel() { PropertyName = "RevokedByIp", PropertyValue = ipAddress },
                new PatchModel() { PropertyName = "ReasonOfRevoke", PropertyValue = reason },
                new PatchModel() { PropertyName = "ReplacedByToken", PropertyValue = replacedByToken },
            });
            await _unitOfWork.Commit();
        }

        //need to  be check
        private async Task RemoveOldRefreshTokens(UserDto userDto)
        {
            await _unitOfWork.RefreshTokens.RemoveRange(token => !token.IsActive && token.UserId.Equals(userDto.Id));
            await _unitOfWork.Commit();
        }

        private async Task<RefreshTokenDto> RotateRefreshToken(RefreshToken token, string ipAddress)
        {
            var newRefreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            await RevokeRefreshToken(token, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;

        }
    }
}