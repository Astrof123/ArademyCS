using System.ComponentModel.DataAnnotations;

namespace Project {

    public class StudentCreateDTO {
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
    }

    public class StudentUpdateDTO {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }
    }

    public class CourseCreateDTO {
        [Required]
        [MaxLength(200)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class CourseUpdateDTO {
        [MaxLength(200)]
        public string? CourseName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }

    public class CourseEnrollmentDTO {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }
    }

    public class TeacherCourseDTO {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }

    public class TeacherCreateDTO {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }

    public class TeacherUpdateDTO {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }
    }

    public class CourseEnrollmentCreateDTO {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }


    }

    public class CourseEnrollmentUpdateDTO {
        public string? Grade { get; set; }
    }

    public class TeacherCourseCreateDTO {
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class CourseEnrollmentResponseDTO {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class CourseDTO {
        public int CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class StudentDTO {
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
    }

    public class TeacherCourseResponseDTO {
        public int TeacherCourseId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }


}
