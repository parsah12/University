using Moq;
using University.User.Application.Requests;
using University.User.Application.Dto.Enum;
using University.User.Model.IRepository;
using University.User.Model.Entities;
using University.User.ApplicationService.Services;
using University.User.Application.IServices;

namespace UnitTest;

public class UserUnitTest : BaseUnitTest
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IRedisService> _redisService;
    private readonly UserService _userService;

    public UserUnitTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        _userService = new UserService(_userRepositoryMock.Object, null!, _redisService!.Object);
    }

    [Fact]
    public void GetUserByUserNameReturnsUserDto()
    {


        IntializeData(DateTime.Now);
        var service = GetUserService();
        var addRes = service.AddUser(new UserRequest
        {
            Role = RoleEnumDto.Admin,
            Age = 10,
            EntryDate = DateTime.Now,
            FirstName = "Asghar",
            LastName = "Asghari",
            FieldOfStudy = "Ghoran",
            MeliCode = "12345",
            UserName = "a.asghari"

        });
        var res = service.GetUserbyUserName("a.asghari");

        Assert.NotNull(res);
        Assert.Equal("a.asghari", res.UserName);
        Assert.Equal("Asghar", res.FirstName);
        Assert.Equal("Asghari", res.LastName);
        Assert.Equal(10, res.Age);
        Assert.Equal(RoleEnumDto.Admin, res.Role);
        Assert.Equal("12345", res.MeliCode);
        Assert.Equal("Ghoran", res.FieldOfStudy);
        Assert.True(addRes);

    }

    [Fact]
    public void GetUserByUserName_UsernameIsNull_ReturnsUserDto()
    {

        _userEntities.Add(new UserEntity
        {
            Age = 10,
            FirstName = "Asghar",
            LastName = "Asghari",
            Role = RoleEnum.Admin,
            UserName = ""
        });
        InitializeRepository();
        var service = GetUserService();
        var res = service.GetUserbyUserName("");

        Assert.Null(res);

    }

    [Fact]
    public void UnitSelection_CallsUserRepositoryWithCorrectParameters()
    {

        var unitSelectionRequest = new UnitSelectionRequest
        {
            UserId = 1,
            TeacherId = 1,
            CourseId = 1,
        };
        IntializeData(DateTime.Now);

        var service = GetUserService();
        service.UnitSelection(unitSelectionRequest);

        var adminService = GetAdminService();
        var res = adminService.GetUnitSelectionBy(unitSelectionRequest.UserId);

        Assert.NotNull(res);
    }

    [Fact]
    public void GetAllUserReturnUserDto()
    {
        IntializeData(DateTime.Now);
        var service = GetUserService();
        var res = service.GetAllUsers();
        Assert.NotNull(res);
        Assert.Equal(3, res.Count);
    }

}