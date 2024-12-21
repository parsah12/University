using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Course.Application.Dto;
using University.Course.Application.IServices;
using University.Course.Application.Requests;


namespace University.Course.Controllers;
[Authorize(Roles ="Admin")] // only admin access to this service
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CourseController> _logger;

    //create constractur
    public CourseController(ICourseService courseService , ILogger<CourseController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    [HttpGet]
    [Route("University/Courses/GetAllCourse")]
    public List<CourseDto>? GetAllCourses()
    {
        _logger.LogInformation("Reading All Courses information");
        return _courseService.GetAllCourses();
    }

    [HttpGet]
    [Route("University/Courses/GetCourse")]
    public CourseDto? GetCourseBy(string courseName)
    {
        _logger.LogInformation("Get Course information by courseName");
        return _courseService.GetCourseBy(courseName);
    }

    [HttpPost]
    [Route("University/Courses/AddCourse")]

    public void AddCourse([FromBody] CourseRequests course)
    {
        _logger.LogInformation("Add new course information by admin");
        _courseService.AddCourse(course);
    }

    [HttpGet]
    [Route("get-cache-coursename")]
    public async Task<string> GetCachedUserName(string courseId)
    {
        _logger.LogInformation("read cached course data from reddis Db ");
        var key = await _courseService.GetCacheCourseAsync(courseId);
        return key ?? "Course not found in cache.";
    }


    [HttpPost]
    [Route("cache-courseName")]
    public async Task<string> CacheCourse(int courseId, string courseName)
    {
        _logger.LogInformation("cache course name in reddis");
        TimeSpan cacheDuriaton = TimeSpan.FromMinutes(10);
        await _courseService.CacheCourseAsync(courseId,courseName , cacheDuriaton);
        return "Cource Name Was Cache Successfully";
    }

















}
