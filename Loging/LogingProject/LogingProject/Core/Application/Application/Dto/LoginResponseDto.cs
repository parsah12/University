﻿using Project.Core.Application.Dto.Enum;

namespace Project.Core.Application.Dto;

public class LoginResponseDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public RoleEnumDto Role { get; set; }
    public string? Token { get; set; }

}
