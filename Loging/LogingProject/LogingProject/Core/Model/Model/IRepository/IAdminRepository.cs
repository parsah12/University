using Project.Core.Model.Entities;


namespace Project.Core.Model.IRepository
{
    public interface IAdminRepository
    {
       
        UserEntity? GetAdminByAdminName(string userName);
        UnitSelectionEntity? GetUnitSelectionBy(int id);
        List<UnitSelectionEntity>? GetAllUnitSelection();
        List<TeacherCourseEntity>? GetAllTeacherCourse();
        TeacherCourseEntity? GetTeacherCourseById(int Id);

    }
}
