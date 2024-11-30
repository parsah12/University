using LogingProject.Dto;
using LogingProject.Entities;
using LogingProject.IRepository;
using LogingProject.IServices;
using LogingProject.Requests;
using LogingProject.Sevices;
using Moq;

namespace TestProject1
{
    public class AdminUnitTest : BaseUnitTest
    {
        private readonly Mock<IAdminRepository> _adminRepositoryMock;
        private readonly AdminService _adminService;
        public AdminUnitTest()
        {
            _adminRepositoryMock = new Mock<IAdminRepository>();
            _adminService = new AdminService(_adminRepositoryMock.Object, null!);
        }


        [Fact]
        public void GetUnitSelections_ReturnsUnitSelectinAsAdminRole()
        {
            IntializeData(DateTime.Now);
            var adminService = GetAdminService();
            var result = adminService.GetAllUnitSelection();
            Assert.NotNull(result);

        }

        [Fact]
        public void GetAllTeacherCourse_ReturnsCorrectTeacherCourseDtos_WhenRepositoryReturnsData()
        {
            IntializeData(DateTime.Now);
            var adminService = GetAdminService();
            var result = adminService.GetAllTeacherCourse();
            Assert.NotNull(result);

        }



        [Fact]
        public void GetUnitSelectionBy_ReturnsCorrectUnitSelectionEntity_WhenIdExists()
        {
          
            int unitSelectionId = 1;
            var mockUnitSelection = new UnitSelectionEntity
            {
                Id = unitSelectionId,
                UserId = 101,
                TeacherId = 202,
                CourseId = 303,
            };
            IntializeData(DateTime.Now);


            var adminService = GetAdminService();
            var result = adminService.GetUnitSelectionBy(mockUnitSelection.Id);

            Assert.NotNull(result);

        }

        [Fact]
        public void UnitSelection_CallsAdminRepositoryWithCorrectParameters()
        {
            var teacherCoursesRequests = new TeacherCourseRequests
            {
                TeacherId = 12,
                CourseId = 1,
                TeacherFirstName = "Sina",
                TeacherLastName = "Saei",
                CourseName = "RapFarsi"
            };

            IntializeData(DateTime.Now);

            var adminService = GetAdminService();
            var res = adminService.GetTeacherCoursesByIdService(teacherCoursesRequests);

            Assert.NotNull(res);
        }
        [Fact]
        public void GetAdminByUserNameReturnsUserDto()
        {
            var expectedAdmin = new AdminDto
            {
                FirstName = "Afshiin",
                LastName = "Amiri",
                UserName = "A.Amiri",
                Age = 42
            };
            IntializeData(DateTime.Now);
            var adminService = GetAdminService();
            var res = adminService.GetAdminByAdminName(expectedAdmin.UserName);
            Assert.NotNull(res);
        }





    }
}
