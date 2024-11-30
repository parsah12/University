using University.User.Application.Dto.Enum;

namespace University.User.Application.Dto
{
    public class TokenValidateDto
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        //public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public RoleEnumDto Role { get; set; }
        public string? Message { get; set; }

        public bool? IsAuthenticated { get; set; }
    }
}