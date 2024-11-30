using Project.Core.Application.Dto;
using Project.Core.Application.Requests;

namespace Project.Core.Application.IServices;

public interface IUserService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
    bool AddUser(UserRequest users);
    List<UserDto> GetAllUsers();
    UserDto? GetUserbyUserName(string userName);
    int UnitSelection(UnitSelectionRequest unitSelection);

}
