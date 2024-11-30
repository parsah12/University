using University.Course.Model.Entities;
using University.Course.Model.IRepository;

namespace University.Course.Infrastructure.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly UniversityContext _universityContext;

    public CourseRepository(UniversityContext universityContext)
    {
        _universityContext = universityContext;
    }


    public List<CourseEntity>? GetAllCourses()
    {
        var res = _universityContext.Courses!.ToList();
        return res;
    }

    public CourseEntity? GetCourseBy(string courseName)
    {
        var course = _universityContext.Courses!.Where(e => e.CourseName == courseName).FirstOrDefault();
        return course!;
    }

    public bool AddCourse(CourseEntity course)
    {
        if (course is not null)
        {
            var entity = new CourseEntity
            {
                CourseName = course.CourseName,
                Capacity = course.Capacity,
                Id = course.Id,
            };
            _universityContext.Courses!.Add(entity);
            _universityContext.SaveChanges();
            return true;
        }
        return false;


    }

}
