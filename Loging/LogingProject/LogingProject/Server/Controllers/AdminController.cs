using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Application.IServices;
using Project.Core.Application.Dto;

namespace server.Controllers
{
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }



        [HttpGet]
        [Route("University/Admins/GetAllUnitSelection")]
        public List<UnitSelectionDto>? GetAllUnitSelection()
        {
            return _adminService.GetAllUnitSelection();
        }

        [HttpGet]
        [Route("University/Admins/GetAdminByAdminName")]

        public AdminDto? GetAdminByAdminName(string username)
        {
            return _adminService?.GetAdminByAdminName(username);
        }

    }

}


