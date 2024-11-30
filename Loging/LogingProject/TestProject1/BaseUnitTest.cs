using LogingProject.Entities;
using LogingProject.IRepository;
using LogingProject.IServices;
using LogingProject.Requests;
using LogingProject.Sevices;
using Microsoft.Extensions.Configuration;
using Moq;

namespace TestProject1;

public class BaseUnitTest
{
    protected List<UserEntity> _userEntities;
    protected List<TeacherCourseEntity> _teacherCourseEntities;
    protected List<UnitSelectionEntity> _unitSelectionEntities;
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IConfiguration> _configuration;
    private readonly Mock<ITeacherRepository> _teacherRepository;
    private readonly Mock<IAdminRepository> _adminRepository;


    public BaseUnitTest()
    {
        _unitSelectionEntities = new List<UnitSelectionEntity>();
        _userEntities = new List<UserEntity>();
        _teacherCourseEntities = new List<TeacherCourseEntity>();
        _userRepository = new Mock<IUserRepository>();
        _tokenService = new Mock<ITokenService>();
        _configuration = new Mock<IConfiguration>();
        _teacherRepository = new Mock<ITeacherRepository>();
        _adminRepository = new Mock<IAdminRepository>();
    }

    public void InitializeRepository()
    {
        RepositoryMockSetup();
    }


    public void IntializeData(DateTime date)
    {
        //ConfigurationMockSetup();
        RepositoryMockSetup();
        GenerateData(date);


    }


    public AdminService GetAdminService()
    {
        return new AdminService(
            _adminRepository.Object,
            _teacherRepository.Object
            );
    }

    public UserService GetUserService()
    {
        return new UserService(
            _userRepository.Object,
            _tokenService.Object
            );
    }
    private void ConfigurationMockSetup()
    {
        var _configurationSection = new Mock<IConfigurationSection>();
        _configurationSection.Setup(c => c.Value).Returns("sarand@agah");
        _configuration.Setup(e => e.GetSection("JwtConfig").GetSection("Key")).Returns(_configurationSection.Object);

    }

    private void GenerateData(DateTime date)
    {
        _userEntities.Add(new UserEntity
        {
            Id = 1,
            Password = BCrypt.Net.BCrypt.HashPassword("1qaz@WSX"),
            UserName = "parsafard",
            FirstName = "Amir",
            LastName = "Hossein",
            EntryDate = date.Date,
            Role = RoleEnum.Student,
            Age = 18,
            FieldOfStudy = "riazi",
            MeliCode = "0023631852",
        });
        _userEntities.Add(new UserEntity
        {
            UserName = "A.Nikkhoo",
            FirstName = "Ali",
            LastName = "Nikkhoo",
            Age = 22,
            Role = RoleEnum.Teacher
        });
        _userEntities.Add(new UserEntity
        {
            UserName = "A.Amiri",
            FirstName = "Afshin",
            LastName = "Amiri",
            Age = 42,
            Role = RoleEnum.Admin,
        });
    }


    private void RepositoryMockSetup()
    {
        _userRepository.Setup(e => e.GetAllUsers()).Returns(_userEntities);
        _userRepository.Setup(e => e.GetUserBy(It.IsAny<string>())).Returns<string>(GetUserBy);
        _userRepository.Setup(e => e.AddUser(It.IsAny<UserRequest>())).Returns<UserRequest>(AddUserMock);
        _userRepository.Setup(repo => repo.UnitSelection(It.IsAny<UnitSelectionEntity>())).Returns<UnitSelectionEntity>(UnitSelection);
        _adminRepository.Setup(repo => repo.GetUnitSelectionBy(It.IsAny<int>())).Returns<int>(id => GetUnitSelectionById(id));
        _adminRepository.Setup(e => e.GetAllUnitSelection()).Returns(_unitSelectionEntities);
        _adminRepository.Setup(e => e.GetTeacherCourseById(It.IsAny<int>())).Returns<int>(GetTeacherCourseByIdRepo);
        _adminRepository.Setup(repo => repo.GetAllTeacherCourse()).Returns(GetAllTeacherCourse());
        _adminRepository.Setup(repo => repo.GetAllUnitSelection()).Returns(GetAllUnitSelectionAsAdminRole);
        _adminRepository.Setup(e => e.GetAdminByAdminName(It.IsAny<string>())).Returns<string>(GetAdminBy);
        _teacherRepository.Setup(e => e.GetTeacherByUsername(It.IsAny<string>()))
            .Returns<string>(username => GetTeacherByUsername(username));
        _teacherRepository.Setup(e => e.GetAllTeachers()).Returns(GetAllTeachers);
        _teacherRepository.Setup(repo => repo.GetUnitSelections()).Returns(GetAllUnitSelectionAsTeacherRole);
    }

    private bool AddUserMock(UserRequest request)
    {
        if(request.UserName is null ) return false;
         _userEntities.Add(new UserEntity 
         { 
             UserName = request.UserName ,
             Age = request.Age ,
             EntryDate = request.EntryDate ,
             FieldOfStudy = request.FieldOfStudy ,
             FirstName = request.FirstName ,
             LastName = request.LastName ,
             MeliCode = request.MeliCode ,
             Id = 10,
             Password = request.Password ,
             Role = RoleEnum.Admin,
             
         });
        return true;
    }

    public List<UnitSelectionEntity>? GetAllUnitSelectionAsTeacherRole()
    {
        return new List<UnitSelectionEntity>()
       {
           new UnitSelectionEntity{ TeacherId = 1 , UserId = 1 , CourseId = 1},
           new UnitSelectionEntity{ TeacherId = 2 , UserId = 2 , CourseId = 2}
       };
    }

    public UserEntity? GetAdminBy(string userName)
    {
        return _userEntities
          .Where(e => e.Role == RoleEnum.Admin && e.UserName == userName)
          .FirstOrDefault();
    }

    private List<UnitSelectionEntity>? GetAllUnitSelectionAsAdminRole()
    {
        return new List<UnitSelectionEntity>()
       {
           new UnitSelectionEntity{ TeacherId = 1 , UserId = 1 , CourseId = 1},
           new UnitSelectionEntity{ TeacherId = 2 , UserId = 2 , CourseId = 2}
       };
    }

    public UserEntity? GetTeacherByUsername(string username)
    {

        return _userEntities
            .Where(e => e.Role == RoleEnum.Teacher && e.UserName == username)
            .FirstOrDefault();
    }

    private UnitSelectionEntity? GetUnitSelectionById(int id)
    {
        return _unitSelectionEntities.FirstOrDefault(u => u.Id == id);
    }


    public List<TeacherCourseEntity>? GetAllTeacherCourse()
    {
        return new List<TeacherCourseEntity>
        {
            new TeacherCourseEntity { TeacherId = 1, CourseId = 101, TeacherFirstName = "John", TeacherLastName = "Doe", CourseName = "Math" },
            new TeacherCourseEntity { TeacherId = 2, CourseId = 102, TeacherFirstName = "Jane", TeacherLastName = "Smith", CourseName = "Science" }
        };
    }


    public int UnitSelection(UnitSelectionEntity selectionRequest)
    {
        _unitSelectionEntities.Add(selectionRequest);
        return 1;
    }


    public UserEntity? GetUserBy(string username)
    {
        return _userEntities.SingleOrDefault(e => e.UserName == username);
    }


    public TeacherService GetTeacherService()
    {
        return new TeacherService
            (
                _teacherRepository.Object,
                _tokenService.Object
            );
    }


    public TeacherCourseEntity? GetTeacherCourseByIdRepo(int Id)
    {

        var teacherCoursesRequests = new TeacherCourseRequests
        {
            TeacherId = 12,
            CourseId = 1,
            TeacherFirstName = "Sina",
            TeacherLastName = "Saei",
            CourseName = "RapFarsi"
        };
        return _teacherCourseEntities.SingleOrDefault(e => e.Id == Id);
    }

    public List<UserEntity>? GetAllTeachers()
    {
        return _userEntities;
    }





}