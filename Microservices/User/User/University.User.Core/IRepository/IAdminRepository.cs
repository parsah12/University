using University.User.Model.Entities;


namespace University.User.Model.IRepository
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
