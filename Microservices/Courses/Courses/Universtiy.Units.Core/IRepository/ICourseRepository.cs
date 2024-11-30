using Project.Core.Model.Entities;
using System.Numerics;

namespace Universtiy.Units.Model.IRepository;

public interface ICourseRepository
{
    List<CourseEntity>? GetAllCourses();
    CourseEntity? GetCourseBy(string courseName);

    bool AddCourse(CourseEntity course);
}