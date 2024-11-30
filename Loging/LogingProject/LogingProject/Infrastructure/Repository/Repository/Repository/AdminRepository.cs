
using Project.Core.Model.Entities;
using Project.Core.Model.IRepository;

namespace Project.Infrastructure.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UniversityContext _universityContext;
        public AdminRepository(UniversityContext universityContext)
        {
            _universityContext = universityContext;
        }

        public UserEntity? GetAdminByAdminName(string userName)
        {
            var res = _universityContext.Users!.Where(x => x.Role == RoleEnum.Admin && x.UserName == userName).FirstOrDefault();
            return res;
        }

        public List<UnitSelectionEntity>? GetAllUnitSelection()
        {
            var res = _universityContext.UnitSelections!.ToList();
            return res;
        }
        public UnitSelectionEntity? GetUnitSelectionBy(int id)
        {
            return _universityContext.UnitSelections!.FirstOrDefault(x => x.Id == id);
        }

        public List<TeacherCourseEntity>? GetAllTeacherCourse()
        {
            var res = _universityContext.TeacherCourses!.ToList();
            return res;
        }

        public TeacherCourseEntity? GetTeacherCourseById(int Id)
        {
            var res = _universityContext.TeacherCourses!.FirstOrDefault(e => e.Id == Id);
            return res;
        }
    }
}
