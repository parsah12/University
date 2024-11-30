namespace Project.Core.Application.Dto
{
    public class TeacherCourseDto
    {
        public int TeacherId { get; set; }
        public string? TeacherFirstName { get; set; }
        public string? TeacherLastName { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }


    }
}
