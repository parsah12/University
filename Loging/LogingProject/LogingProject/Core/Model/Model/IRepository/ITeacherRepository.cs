
using Project.Core.Model.Entities;

namespace Project.Core.Model.IRepository
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
