using Project.Core.Application.Dto.Enum;

namespace Project.Core.Application.Requests
{
    public class UserRequest
    {

        public string? UserName { get; set; }
        public RoleEnumDto Role { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? MeliCode { get; set; }
        public string? FieldOfStudy { get; set; }
        public DateTime EntryDate { get; set; }

    }
}
