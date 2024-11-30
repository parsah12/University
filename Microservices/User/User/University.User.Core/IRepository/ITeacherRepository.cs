using University.User.Model.Entities;

namespace University.User.Model.IRepository
{
    public interface ITeacherRepository
    {
        void AddTeacher(UserEntity teacher);
        List<UserEntity> GetAllTeachers();
        List<UserEntity>? GetAllTeachersCourses();
        UserEntity? GetTeacherByUsername(string username);
        List<UnitSelectionEntity>? GetUnitSelections();
    }
}
