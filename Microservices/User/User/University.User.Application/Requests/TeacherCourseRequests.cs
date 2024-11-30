namespace University.User.Application.Requests
{
    public class TeacherCourseRequests
    {
        public int TeacherId { get; set; }
        public string? TeacherFirstName { get; set; }
        public string? TeacherLastName { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
