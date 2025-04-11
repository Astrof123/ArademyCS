using System.ComponentModel.DataAnnotations;

namespace Project.Entities {
    public class CourseEnrollment {
        [Key]
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string Grade { get; set; }
    }
}
