using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Course.Application.Dto;
using University.Course.Application.IServices;
using University.Course.Application.Requests;
using University.Course.Protos;
using University.Units.Protos;


namespace University.Course.Controllers;
[Authorize]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [Route("University/Courses/GetAllCourse")]
    public List<CourseDto>? GetAllCourses()
    {
        return _courseService.GetAllCourses();
    }

    [HttpGet]
    [Route("University/Courses/GetCourse")]
    public CourseDto? GetCourseBy(string courseName)
    {
        return _courseService.GetCourseBy(courseName);
    }

    [HttpPost]
    [Route("University/Courses/AddCourse")]

    public void AddCourse([FromBody] CourseRequests course)
    {
        _courseService.AddCourse(course);
    }

    [HttpGet]
    [Route("get-cache-coursename")]
    public async Task<string> GetCachedUserName(string courseId)
    {
        var key = await _courseService.GetCacheCourseAsync(courseId);
        return key ?? "Course not found in cache.";
    }


    [HttpPost]
    [Route("cache-courseName")]
    public async Task<string> CacheCourse(int courseId, string courseName)
    {
        await _courseService.CacheCourseAsync(courseId,courseName);
        return "Cource Name Was Cache Successfully";
    }


















    //[HttpPost]
    //[Route("University/Courses/UnitProto")]
    //public async Task TestUnitProto()
    //{
    //    try
    //    {
    //        var channel = GrpcChannel.ForAddress("http://localhost:5002");
    //        var client = new UnitSelectionGrp.UnitSelectionGrpClient(channel);

    //        var request = new UnitRequests
    //        {
    //            Courseid = 1,
    //            Teacherid = 1,
    //            Userid = 1,
    //        };

    //        var response = await client.UnitSelectionAsync(request, default);

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}

    //[HttpPost]
    //[Route("University/Courses/UserProto")]

    //public async Task TestUserProto()
    //{
    //    try
    //    {
    //        var channel = GrpcChannel.ForAddress("http://localhost:5008");
    //        var client = new UsersGrpc.UsersGrpcClient(channel);

    //        var requests = new UserRequests
    //        {
    //            Age = 1,
    //            Firstname = "parsa",
    //            Lastname = "hedayati",
    //            Id = 1,
    //            Username = "PWRSW82"
    //        };

    //        var response = await client.UsersAsync(requests, default);
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
        
    //}

}
