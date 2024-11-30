using Project.Core.Application.Dto;
using Project.Core.Application.Requests;

namespace Project.Core.Application.IServices
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
