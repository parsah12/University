
using BCrypt.Net;
using Project.Core.Application.Dto;
using Project.Core.Application.IServices;
using Project.Core.Application.Requests;
using Project.Core.Model.Entities;
using Project.Core.Model.IRepository;

namespace Project.Core.ApplicationService.Sevices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public bool AddUser(UserRequest users)
    {
        var entity = new UserEntity 
        {
            Age = users.Age ,
            EntryDate = users.EntryDate ,
            FieldOfStudy = users.FieldOfStudy ,
            FirstName = users.FirstName ,
            LastName = users.LastName ,
            MeliCode = users.MeliCode ,
            Password = users.Password ,
            Role = RoleEnum.Student,
            UserName = users.UserName 
        };
        return _userRepository.AddUser(entity);
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
            Role = (Application.Dto.Enum.RoleEnumDto)x.Role,
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
            Role = (Application.Dto.Enum.RoleEnumDto)res.Role,
            UserName = res.UserName,
        };
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            throw new ArgumentNullException();

        var user = _userRepository.GetUserBy(loginRequest.UserName);
        if (user is null)throw new ArgumentException();

        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.Password, user.Password);
        if (!isPasswordValid) throw new Exception("Incorrect Pass");
        var userDto = new UserDto { Age = user.Age };
        var token = await _tokenService.GenerateToken(userDto);

        return new LoginResponseDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = (Application.Dto.Enum.RoleEnumDto)user.Role,
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
}
