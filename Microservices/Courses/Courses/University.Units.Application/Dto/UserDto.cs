﻿using University.Course.Application.Dto.Enum;

namespace University.Course.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public RoleEnumDto Role { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? MeliCode { get; set; }
        public string? FieldOfStudy { get; set; }

        
    }
}
