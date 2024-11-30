namespace Project.Core.Model.Entities
{
    public class UnitSelectionEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public virtual UserEntity? Users { get; set; }
        public int CourseId { get; set; }
        public virtual CourseEntity? Courses { get; set; }


    }
}
