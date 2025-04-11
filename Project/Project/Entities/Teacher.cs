using System.ComponentModel.DataAnnotations;

namespace Project.Entities {
    public class Teacher {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
