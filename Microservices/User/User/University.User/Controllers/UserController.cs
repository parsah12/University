using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.User.Application.Dto;
using University.User.Application.IServices;
using University.User.Application.Requests;
using University.User.Model.IRepository;

namespace University.User.Controllers
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



        [HttpGet]
        [Route("University/Users/GetUserByUserName")]
        public UserDto? GetUserByUserName(string userName)
        {
            return _userService.GetUserbyUserName(userName);
        }


        [HttpPost]
        [Route("Cahse-UserName")]
        public async Task<IActionResult> CasheUserName(int userId, string userName)
        {
            await _userService.CashUserNameAsynck(userId, userName);
            return Ok("User Name was Chashe Successfully");
        }







        //[HttpGet]
        //[Route("get-cashed-userName")]

        //public async Task<string> GetCashedUserNameAsynckBy(int userId)
        //{
        //    var userName = await _userService.GetCashedUserNameAsynck(userId);
        //    if (userName == null)

        //    {
        //        throw new NotImplementedException("User was not found");
        //    }

        //    return userName;
        //}


    }
}






