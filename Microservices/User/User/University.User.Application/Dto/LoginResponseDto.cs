using University.User.Application.Dto.Enum;

namespace University.User.Application.Dto;

public class LoginResponseDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public RoleEnumDto Role { get; set; }
    public string? Token { get; set; }
}
