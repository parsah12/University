using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.User.Application.Dto;
using University.User.Application.IServices;
using University.User.Application.Requests;

namespace University.User.Controllers
{
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
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
        public async Task<string> CacheUserName(int userId, string userName)
        {
            TimeSpan cacheDuriation = TimeSpan.FromMinutes(10);
            await _userService.CacheUserNameAsync(userId, userName,cacheDuriation );
            return "User name cached successfully.";
        }



        [HttpGet]
        [Route("get-cashed-userName")]

        public async Task<string> GetCachedUserName(string userId)
        {
            var key = await _userService.GetCachedUserNameAsync(userId);
            return key ?? "User not found in cache.";
        }






    }
}






