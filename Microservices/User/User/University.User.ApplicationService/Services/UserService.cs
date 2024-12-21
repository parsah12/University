using University.User.Application.Dto;
using University.User.Application.Dto.Enum;
using University.User.Application.IServices;
using University.User.Application.Requests;
using University.User.Model.Entities;
using University.User.Model.IRepository;

namespace University.User.ApplicationService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRedisService _redisService;


    public UserService(IUserRepository userRepository, ITokenService tokenService, IRedisService redisService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _redisService = redisService;
    }

    public bool AddUser(UserRequest users)
    {
        var generatTokent = _tokenService.GenerateToken;
        if (generatTokent is not null)
        {
            var entity = new UserEntity
            {
                Age = users.Age,
                EntryDate = users.EntryDate,
                FieldOfStudy = users.FieldOfStudy,
                FirstName = users.FirstName,
                LastName = users.LastName,
                MeliCode = users.MeliCode,
                Password = users.Password,
                Role = (RoleEnum)users.Role,
                UserName = users.UserName
            };
            return _userRepository.AddUser(entity);
        }
        return false;
    }

    public List<UserDto> GetAllUsers()
    {
        var res = _userRepository.GetAllUsers();
        return res.Select(x => new UserDto
        {
            Age = x.Age,
            FieldOfStudy = x.FieldOfStudy,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Id = x.Id,
            MeliCode = x.MeliCode,
            Role = (RoleEnumDto)x.Role,
            UserName = x.UserName,
        }).ToList();
    }

    public UserDto? GetUserbyUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName)) return null;
        var res = _userRepository.GetUserBy(userName);
        if (res is null) return null;
        return new UserDto
        {
            Age = res.Age,
            FieldOfStudy = res.FieldOfStudy,
            FirstName = res.FirstName,
            LastName = res.LastName,
            Id = res.Id,
            MeliCode = res.MeliCode,
            Role = (RoleEnumDto)res.Role,
            UserName = res.UserName,
        };
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            throw new ArgumentNullException();

        var user = _userRepository.GetUserBy(loginRequest.UserName);
        if (user is null) throw new ArgumentException();

        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.Password, user.Password);
        if (!isPasswordValid) throw new Exception("Incorrect Pass");
        var userDto = new UserDto { Age = user.Age, FirstName = user.FirstName, LastName = user.LastName, FieldOfStudy = user.FieldOfStudy, UserName = user.UserName, Id = user.Id, Role = (RoleEnumDto)user.Role, MeliCode = user.MeliCode };
        var token = await _tokenService.GenerateToken(userDto);

        return new LoginResponseDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = (RoleEnumDto)user.Role,
            Token = token,
        };
    }

    public int UnitSelection(UnitSelectionRequest unitSelection)
    {
        var entity = new UnitSelectionEntity
        {
            UserId = unitSelection.UserId,
            TeacherId = unitSelection.TeacherId,
            CourseId = unitSelection.CourseId
        };
        return _userRepository.UnitSelection(entity);
    }

    public async Task CacheUserNameAsync(int userId, string userName, TimeSpan cacheDuration)
    {
        var users = GetAllUsers();
      

        foreach (var user in users)
        {
            string key = $"user:{user.Id}:name";
            var value = user.UserName;
            await _redisService.SetValueAsync(key, value, cacheDuration);
        }
    }

    public async Task<string> GetCachedUserNameAsync(string userId)
    {
        string key = userId;
        return await _redisService.GetValueAsync(key);
    }


}
