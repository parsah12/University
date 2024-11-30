using University.Course.Application.Dto;
using University.Course.Application.IServices;
using University.Course.Application.Requests;
using University.Course.Model.Entities;
using University.Course.Model.IRepository;

namespace University.Course.ApplicationServices.Services;
public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;


    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public List<CourseDto>? GetAllCourses()
    {
        var result = _courseRepository.GetAllCourses();
        return result!.Select(x => new CourseDto
        {
            CourseName = x.CourseName,
            Capacity = x.Capacity,
            Id = x.Id,
        }).ToList();
    }

    public CourseDto? GetCourseBy(string courseName)
    {
        if (string.IsNullOrEmpty(courseName)) return null;
        var res = _courseRepository.GetCourseBy(courseName);
        if (res is null) return null;
        return new CourseDto
        {
            CourseName = courseName,
            Capacity = res.Capacity,
            Id = res.Id
        };
    }

    public bool AddCourse(CourseRequests course)
    {
        var entity = new CourseEntity
        {
            CourseName = course.CourseName,
            Capacity = course.Capasity,

        };
        return _courseRepository.AddCourse(entity);
    }

    //public void GetGrpcService()
    //{

    //}
}
