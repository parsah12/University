using LogingProject.Dto;
using LogingProject.Entities;
using LogingProject.IRepository;
using LogingProject.Requests;
using LogingProject.Sevices;
using Moq;

namespace TestProject1
{
    public class TeacherUnitTest : BaseUnitTest
    {
        private readonly Mock<ITeacherRepository> _teacherRepository;
        private readonly TeacherService _teacherService;
        public TeacherUnitTest()
        {
            _teacherRepository = new Mock<ITeacherRepository>();
            _teacherService = new TeacherService(_teacherRepository.Object, null!);
        }


        [Fact]
        public void GetTeacherByUserNameReturnsUserDto()
        {

            var expectedTeacher = new TeacherDto
            {
                FirstName = "Ali",
                LastName = "Nikkhoo",
                UserName = "A.Nikkhoo",
                Age = 22
            };

            IntializeData(DateTime.Now);
            var teacherService = GetTeacherService();
            var result = teacherService.GetTeacherByUsername(expectedTeacher.UserName);
            Assert.NotNull(result);
           
        }




        [Fact]
        public void GetUnitSelections_ReturnsUnitSelectionAsTeacherRole()
        {

            IntializeData(DateTime.Now);

            var teacherService = GetTeacherService();
            var res = teacherService.GetUnitSelections();
            Assert.NotNull(res);
        }

        [Fact]
        public void GetAllTeacherReturnsUserDto()
        {
            IntializeData(DateTime.Now);

            var service = GetTeacherService();
            var res = service.GetAllTeachers();
            Assert.NotNull(res);
         
        }
    }
}
