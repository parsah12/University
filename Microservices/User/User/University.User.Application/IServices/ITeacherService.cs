using University.User.Application.Dto;
using University.User.Application.Requests;

namespace University.User.Application.IServices
{
    public interface ITeacherService
    {
        void AddTeacher(TeacherRequest teacher);
        List<TeacherDto>? GetAllTeachers();
        List<TeacherCourseDto>? GetAllTeachersCourses();
        TeacherDto? GetTeacherByUsername(string username);
        List<UnitSelectionDto>? GetUnitSelections();
    }
}
