using System.ComponentModel.DataAnnotations;

namespace Project.Entities {
    public class Course {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
