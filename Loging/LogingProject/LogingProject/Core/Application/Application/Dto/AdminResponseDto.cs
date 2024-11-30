using Project.Core.Application.Dto.Enum;

namespace Project.Core.Application.Dto
{
    public class AdminResponseDto
    {
        public int AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public RoleEnumDto Role { get; set; }
        public string? Token { get; set; }
    }
}
