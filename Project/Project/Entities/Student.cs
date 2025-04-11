using System.ComponentModel.DataAnnotations;

namespace Project.Entities {
    public class Student {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
