﻿using University.Course.Application.Dto;
using University.Course.Application.Requests;

namespace University.Course.Application.IServices
{
    public interface ICourseService
    {
        List<CourseDto>? GetAllCourses();
        CourseDto? GetCourseBy(string courseName);
        bool AddCourse(CourseRequests course);

    }
}