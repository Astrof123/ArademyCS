using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System.Collections.Generic;

namespace Project {
    public class AppDbContext : DbContext {
        public AppDbContext() => Database.EnsureCreated();

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<TeacherCourse>()
                .HasKey(tc => new { tc.TeacherCourseId });


            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(ce => ce.StudentId);

            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(ce => ce.CourseId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourses)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(tc => tc.CourseId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
