using University.User.Application.Dto;
using University.User.Application.Dto.Enum;
using University.User.Application.IServices;
using University.User.Application.Requests;
using University.User.Model.Entities;
using University.User.Model.IRepository;

namespace University.User.ApplicationService.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository, ITokenService @object)
        {
            _teacherRepository = teacherRepository;

        }

        public void AddTeacher(TeacherRequest teacher)
        {
            var entity = new UserEntity
            {
                Age = teacher.Age,
                EntryDate = teacher.EntryDate,
                FieldOfStudy = teacher.FieldOfStudy,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MeliCode = teacher.MeliCode,
                UserName = teacher.UserName
            };
            _teacherRepository.AddTeacher(entity);
        }

        public List<TeacherDto> GetAllTeachers()
        {
            var res = _teacherRepository.GetAllTeachers();
            return res.Select(x => new TeacherDto
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

        public List<TeacherCourseDto>? GetAllTeachersCourses()
        {
            var res = _teacherRepository.GetAllTeachersCourses();
            if (res is null) return null;
            return res.Select(t => new TeacherCourseDto
            {
                TeacherId = t.Id,
                TeacherFirstName = t.FirstName,
                TeacherLastName = t.LastName,
                CourseId = t.TeacherCourse!.FirstOrDefault(x => x.TeacherId == t.Id).Course.Id,
                CourseName = t.TeacherCourse!.FirstOrDefault(x => x.TeacherId == t.Id).Course.CourseName
            }).ToList();

        }

        public TeacherDto? GetTeacherByUsername(string username)
        {
            var res = _teacherRepository.GetTeacherByUsername(username);
            if (res is null) return null;
            return new TeacherDto
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

        public List<UnitSelectionDto>? GetUnitSelections()
        {
            var result = _teacherRepository.GetUnitSelections();
            return result!.Select(x => new UnitSelectionDto
            {
                UserId = x.UserId,
                TeacherId = x.TeacherId,
                CourseId = x.CourseId,
            }).ToList();
        }
    }

}
