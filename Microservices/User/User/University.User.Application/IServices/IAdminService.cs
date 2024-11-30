using University.User.Application.Dto;
using University.User.Application.Requests;

namespace University.User.Application.IServices
{
    public interface IAdminService
    {
        //void AddAdmin(AdminRequest admin);

        List<UnitSelectionDto>? GetAllUnitSelection();
        UnitSelectionDto? GetUnitSelectionBy(int id);
        AdminDto? GetAdminByAdminName(string userName);
        List<TeacherCourseDto>? GetAllTeacherCourse();
        TeacherCourseDto? GetTeacherCoursesByIdService(TeacherCourseRequests teacherCourseRequests);
    }
}
