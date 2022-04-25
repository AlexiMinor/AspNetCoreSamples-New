using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface ITokenService
{
    Task RevokeToken(string token, string ipAddress);
    Task<JwtAuthDto> RefreshToken(string? refreshToken, string ipAddress);
    Task<JwtAuthDto> GetToken(LoginDto request, string getIpAddress);
}