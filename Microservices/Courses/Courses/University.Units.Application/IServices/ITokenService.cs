using University.Course.Application.Dto;

namespace University.Course.Application.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserDto user);

        Task<TokenValidateDto> ValidateToken(string token);
    }
}
