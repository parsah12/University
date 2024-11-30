using Project.Core.Application.Dto.Enum;

namespace Project.Core.Application.Requests
{
    public class AdminRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AdminName { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public RoleEnumDto Role { get; set; }
    }
}
