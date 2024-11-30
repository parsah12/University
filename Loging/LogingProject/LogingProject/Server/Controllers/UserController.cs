using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Application.Dto;
using Project.Core.Application.IServices;
using Project.Core.Application.Requests;
using Project.Core.Model.Entities;
using Project.Core.Model.IRepository;

namespace server.Controllers
{
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserRepository userRepository, IUserService userService)
        {

            _userService = userService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("University/Users/AddUser")]
        public void AddUser([FromBody] UserRequest users)
        {
            _userService.AddUser(users);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("University/Users/GetAllUsers")]
        public List<UserDto> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("University/Users/Login")]
        public async Task<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequest)
        {
            return await _userService.Login(loginRequest);
        }


        [HttpPost]
        [Route("University/Users/UnitSelection")]
        public void UnitSelection([FromBody] UnitSelectionRequest unitSelection)
        {
            _userService.UnitSelection(unitSelection);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("University/Users/GetUserByUserName")]
        public UserDto? GetUserByUserName(string userName)
        {
            return _userService.GetUserbyUserName(userName);
        }



    }
}






