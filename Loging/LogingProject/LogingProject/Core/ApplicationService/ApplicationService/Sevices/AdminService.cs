using Project.Core.Application.Dto;
using Project.Core.Application.Dto.Enum;
using Project.Core.Application.IServices;
using Project.Core.Application.Requests;
using Project.Core.Model.Entities;
using Project.Core.Model.IRepository;


namespace Project.Core.ApplicationService.Sevices
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AdminService(IAdminRepository adminRepository, ITeacherRepository teacherRepository)
        {
            _adminRepository = adminRepository;
            _teacherRepository = teacherRepository;
        }
        //public void AddAdmin(AdminRequest admin)
        //{
        //    var entity = new UserEntity {Age = admin.Age};
        //    _adminRepository.AddAdmin(entity);
        //}
        public AdminDto? GetAdminByAdminName(string userName)
        {
            var res = _adminRepository.GetAdminByAdminName(userName);
            if (res is null) return null;
            return new AdminDto
            {
                UserName = res.UserName,
                FirstName = res.FirstName,
                LastName = res.LastName,
                Age = res.Age,
                Role = (RoleEnumDto)res.Role,
                Id = res.Id,
            };
        }

        public List<UnitSelectionDto>? GetAllUnitSelection()
        {
            var res = _adminRepository.GetAllUnitSelection();
            if (res is null) return null;
            return res.Select(x => new UnitSelectionDto
            {
                UserId = x.UserId,
                TeacherId = x.TeacherId,
                CourseId = x.CourseId,
            }).ToList();
        }

        public UnitSelectionDto? GetUnitSelectionBy(int id)
        {
            var res = _adminRepository.GetUnitSelectionBy(id);
            return new UnitSelectionDto
            {
                UserId = id,
                TeacherId = id,
                CourseId = id,
            };
        }



        public List<TeacherCourseDto>? GetAllTeacherCourse()
        {
            var res = _adminRepository.GetAllTeacherCourse();
            if (res is null) return null;
            return res.Select(x => new TeacherCourseDto
            {
                TeacherId = x.TeacherId,
                CourseId = x.CourseId,
                TeacherFirstName = x.TeacherFirstName,
                TeacherLastName = x.TeacherLastName,
                CourseName = x.CourseName,
            }).ToList();
        }

        public TeacherCourseDto? GetTeacherCoursesByIdService(TeacherCourseRequests teacherCourseRequests)
        {
            var res = _adminRepository.GetTeacherCourseById(teacherCourseRequests.TeacherId);
            return new TeacherCourseDto
            {
                TeacherId = teacherCourseRequests.TeacherId,
                CourseId = teacherCourseRequests.CourseId,
                TeacherFirstName = teacherCourseRequests.TeacherFirstName,
                TeacherLastName = teacherCourseRequests.TeacherLastName,
                CourseName = teacherCourseRequests.CourseName
            };
        }

        public object GetTeacherCoursesByIdService()
        {
            throw new NotImplementedException();
        }
    }
}
