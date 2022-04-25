using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(UserDto user);
    public Task<Guid?> ValidateJwtToken(string token);
    public RefreshTokenDto GenerateRefreshToken(string ipAdress);
}