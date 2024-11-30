using University.User.Application.Dto;

namespace University.User.Application.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserDto user);

        Task<TokenValidateDto> ValidateToken(string token);

    }
}
