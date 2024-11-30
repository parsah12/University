using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project.Core.Application.IServices;
using Project.Core.Application.Dto;

namespace server.Controllers
{
    [Authorize]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {

            _teacherService = teacherService;
        }



        [HttpGet]
        [Route("University/Teachers/GetAllTeachers")]
        public List<TeacherDto>? GetAllTeachers()
        {
            return _teacherService.GetAllTeachers();
        }


        [HttpGet]
        [Route("University/Teachers/GetAllTeachersCourses")]
        public List<TeacherCourseDto>? GetAllTeachersCourses()
        {
            return _teacherService.GetAllTeachersCourses();
        }

        [HttpGet]
        [Route("University/Teachers/GetTeacherByTeacherName")]
        public TeacherDto? GetTeacherByTeacherName(string username)
        {
            return _teacherService.GetTeacherByUsername(username);
        }

        [HttpGet]
        [Route("Uinversity/Teachers/GetUnitselection")]
        public List<UnitSelectionDto>? GetUnitSelection()
        {
            return _teacherService.GetUnitSelections();
        }

    }
}
