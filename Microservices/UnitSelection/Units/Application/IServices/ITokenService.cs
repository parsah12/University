using Application.Dto;

namespace Application.IServices;

public interface ITokenService
{
    Task<string> GenerateToken(UserDto user);

    Task<TokenValidateDto> ValidateToken(string token);

}
