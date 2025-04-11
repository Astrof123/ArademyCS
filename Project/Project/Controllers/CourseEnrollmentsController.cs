using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollmentsController : ControllerBase {
        private readonly AppDbContext _context;

        public CourseEnrollmentsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseEnrollmentResponseDTO>> GetCourseEnrollment(int id) {
            var courseEnrollment = await _context.CourseEnrollments.FindAsync(id);

            if (courseEnrollment == null) {
                return NotFound();
            }

            var responseDto = new CourseEnrollmentResponseDTO {
                EnrollmentId = courseEnrollment.EnrollmentId,
                StudentId = courseEnrollment.StudentId,
                CourseId = courseEnrollment.CourseId,
                Grade = courseEnrollment.Grade,
                EnrollmentDate = courseEnrollment.EnrollmentDate
            };

            return responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<CourseEnrollment>> PostCourseEnrollment(CourseEnrollmentCreateDTO enrollmentDto) {
            var student = await _context.Students.FindAsync(enrollmentDto.StudentId);
            var course = await _context.Courses.FindAsync(enrollmentDto.CourseId);

            if (student == null || course == null) {
                return BadRequest("Invalid StudentId or CourseId");
            }

            var courseEnrollment = new CourseEnrollment {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
                EnrollmentDate = DateTime.Now,
                Grade = "5"
            };

            _context.CourseEnrollments.Add(courseEnrollment);
            await _context.SaveChangesAsync();

            var responseDto = new CourseEnrollmentResponseDTO {
                EnrollmentId = courseEnrollment.EnrollmentId,
                StudentId = courseEnrollment.StudentId,
                CourseId = courseEnrollment.CourseId,
                Grade = courseEnrollment.Grade,
                EnrollmentDate = courseEnrollment.EnrollmentDate
            };

            return CreatedAtAction(nameof(GetCourseEnrollment), new { id = courseEnrollment.EnrollmentId }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseEnrollment(int id, CourseEnrollmentUpdateDTO enrollmentDto) {
            if (!CourseEnrollmentExists(id)) {
                return NotFound();
            }

            var courseEnrollment = await _context.CourseEnrollments.FindAsync(id);

            if (courseEnrollment == null) {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(enrollmentDto.Grade)) {
                courseEnrollment.Grade = enrollmentDto.Grade;
            }

            _context.Entry(courseEnrollment).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CourseEnrollmentExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseEnrollment(int id) {
            var courseEnrollment = await _context.CourseEnrollments.FindAsync(id);
            if (courseEnrollment == null) {
                return NotFound();
            }

            _context.CourseEnrollments.Remove(courseEnrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseEnrollmentExists(int id) {
            return _context.CourseEnrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
