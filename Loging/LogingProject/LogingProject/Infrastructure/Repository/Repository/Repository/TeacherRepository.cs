using Microsoft.EntityFrameworkCore;
using Project.Core.Model.Entities;
using Project.Core.Model.IRepository;

namespace Project.Infrastructure.Repository.Repository
{

    public class TeacherRepository : ITeacherRepository
    {
        private readonly UniversityContext _universityContext;
        public TeacherRepository(UniversityContext universityContext)
        {
            _universityContext = universityContext;
        }
        public void AddTeacher(UserEntity teacher)
        {
            if (teacher is not null)
            {
                var hashPass = BCrypt.Net.BCrypt.EnhancedHashPassword(teacher.Password, 8); ;
                var entity = new UserEntity
                {
                    UserName = teacher.UserName,
                    Password = hashPass.ToString(),
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Age = teacher.Age,
                    MeliCode = teacher.MeliCode,
                    EntryDate = teacher.EntryDate,
                    FieldOfStudy = teacher.FieldOfStudy,
                    Role = RoleEnum.Teacher
                };
                _universityContext.Users!.Add(entity);
                _universityContext.SaveChanges();
            }
        }

        public List<UserEntity> GetAllTeachers()
        {
            var res = _universityContext.Users!.Where(e => e.Role == RoleEnum.Teacher).ToList();
            return res;
        }

        public List<UserEntity> GetAllTeachersCourses()
        {
            return _universityContext.Users!.Where(e => e.Role == RoleEnum.Teacher).Include(t => t.TeacherCourse!).ThenInclude(t => t.Course!).ToList();

        }



        public UserEntity? GetTeacherByUsername(string username)
        {
            var res = _universityContext.Users!.Where(e => e.Role == RoleEnum.Teacher && e.UserName == username).FirstOrDefault();
            return res;
        }
        public List<UnitSelectionEntity>? GetUnitSelections()
        {
            var res = _universityContext.UnitSelections!.ToList();
            return res;
        }
    }
}
