using University.User.Application.Dto;
using University.User.Application.Requests;

namespace University.User.Application.IServices;

public interface IUserService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
    bool AddUser(UserRequest users);
    List<UserDto> GetAllUsers();
    UserDto? GetUserbyUserName(string userName);
    int UnitSelection(UnitSelectionRequest unitSelection);
}
