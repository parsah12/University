using Project.Core.Application.Dto;
using Project.Core.Application.Requests;

namespace Project.Core.Application.IServices
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
