namespace University.Course.Model.Entities
{
    public class TeacherCourseEntity
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string? TeacherFirstName { get; set; }
        public string? TeacherLastName { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public virtual UserEntity? Teacher { get; set; }
        public virtual CourseEntity? Course { get; set; }
    }
}
