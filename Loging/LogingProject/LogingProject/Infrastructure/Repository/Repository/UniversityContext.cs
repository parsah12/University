using Microsoft.EntityFrameworkCore;
using Project.Core.Model.Entities;

namespace Project.Infrastructure.Repository
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity>? Users { get; set; }
        public DbSet<CourseEntity>? Courses { get; set; }
        public DbSet<UnitSelectionEntity>? UnitSelections { get; set; }
        public DbSet<TeacherCourseEntity>? TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users", "dbo");
            modelBuilder.Entity<CourseEntity>().ToTable("Course", "dbo");
            modelBuilder.Entity<UnitSelectionEntity>().ToTable("UnitSelection", "dbo");
            modelBuilder.Entity<TeacherCourseEntity>().ToTable("TeacherCourse", "dbo");

            modelBuilder.Entity<UnitSelectionEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Users)
                      .WithMany(u => u.UnitSelection)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Courses)
                      .WithMany(c => c.UnitSelection)
                      .HasForeignKey(e => e.CourseId);
            });
        }
    }
}
