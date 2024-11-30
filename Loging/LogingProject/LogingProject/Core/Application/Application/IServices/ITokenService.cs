using Project.Core.Application.Dto;

namespace Project.Core.Application.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserDto user);

    }
}
