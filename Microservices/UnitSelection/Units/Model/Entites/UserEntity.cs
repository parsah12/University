namespace Model.Entites
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public RoleEnum Role { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? MeliCode { get; set; }
        public string? FieldOfStudy { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual List<UnitSelectionEntity>? UnitSelection { get; set; }
        public virtual List<TeacherCourseEntity>? TeacherCourse { get; set; }
    }


}

