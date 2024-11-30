namespace University.User.Model.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public int Capacity { get; set; }
        public virtual List<UnitSelectionEntity>? UnitSelection { get; set; }
        public virtual List<TeacherCourseEntity>? TeacherCourses { get; set; }

    }
}
