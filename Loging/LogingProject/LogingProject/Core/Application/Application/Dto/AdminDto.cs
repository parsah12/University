using Project.Core.Application.Dto.Enum;


namespace Project.Core.Application.Dto
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public int Age { get; set; }
        public RoleEnumDto Role { get; set; }
    }
}
