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
        private readonly ILogger<UserController> _logger;   
        //create constractur
        public UserController(IUserService userService,ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")] // only admin can add new user in database
        [Route("University/Users/AddUser")]
        public void AddUser([FromBody] UserRequest users)
        {
            _logger.LogInformation("add new user information by admin");
            _userService.AddUser(users);
        }

        [HttpGet]
        [Route("University/Users/GetAllUsers")] // Get all user information from data base 
        public List<UserDto> GetAllUsers()
        {
            _logger.LogInformation("read all users information , from database ");
            return _userService.GetAllUsers();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("University/Users/Login")] // user must login for get token to use another services
        public async Task<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequest)
        {
            _logger.LogInformation("Wow you are login");
            return await _userService.Login(loginRequest);
        }



        [HttpGet]
        [Route("University/Users/GetUserByUserName")] // get user information from data base by userName
        public UserDto? GetUserByUserName(string userName)
        {
            _logger.LogInformation("read user data by username");
            return _userService.GetUserbyUserName(userName);
        }


        [HttpPost]
        [Route("Cahse-UserName")] 
        public async Task<string> CacheUserName(int userId, string userName)
        {
            try
            {
                _logger.LogInformation("i try cache data");
                TimeSpan cacheDuriation = TimeSpan.FromMinutes(10);
                await _userService.CacheUserNameAsync(userId, userName, cacheDuriation);
                return "User name cached successfully.";
            }
            catch (Exception)
            {
                _logger.LogError("cache data failed!!");
                return "cache data faile";
            }
           
        }

        [HttpGet]
        [Route("get-cashed-userName")]

        public async Task<string> GetCachedUserName(string userId)
        {
            try
            {
                _logger.LogInformation("read cached data from redis Db");
                var key = await _userService.GetCachedUserNameAsync(userId);
                return key ?? "User not found in cache.";
            }
            catch (Exception)
            {
                _logger.LogError("Erro to read data");
                throw ;
            }
            
        }






    }
}






