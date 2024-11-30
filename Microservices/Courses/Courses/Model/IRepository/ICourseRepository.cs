using University.Course.Model.Entities;

namespace University.Course.Model.IRepository;

public interface ICourseRepository
{
    List<CourseEntity>? GetAllCourses();
    CourseEntity? GetCourseBy(string courseName);

    bool AddCourse(CourseEntity course);
}