using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents() {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id) {
            var student = await _context.Students.FindAsync(id);

            if (student == null) {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDTO studentDto) {
            var student = new Student {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                DateOfBirth = studentDto.DateOfBirth,
                Email = studentDto.Email,
                Phone = studentDto.Phone
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentUpdateDTO studentDto) {
            if (!StudentExists(id)) {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null) {
                return NotFound();
            }

            // Update only the properties that are present in the DTO
            if (!string.IsNullOrEmpty(studentDto.FirstName)) {
                student.FirstName = studentDto.FirstName;
            }
            if (!string.IsNullOrEmpty(studentDto.LastName)) {
                student.LastName = studentDto.LastName;
            }

            if (studentDto.DateOfBirth.HasValue) {
                student.DateOfBirth = studentDto.DateOfBirth.Value;
            }
            if (!string.IsNullOrEmpty(studentDto.Email)) {
                student.Email = studentDto.Email;
            }
            if (!string.IsNullOrEmpty(studentDto.Phone)) {
                student.Phone = studentDto.Phone;
            }

            _context.Entry(student).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!StudentExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id) {
            var student = await _context.Students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetStudentCourses(int id) {
            var student = await _context.Students
                .Include(s => s.CourseEnrollments)
                .ThenInclude(ce => ce.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) {
                return NotFound();
            }

            var courses = student.CourseEnrollments.Select(ce => new CourseDTO {
                CourseId = ce.Course.CourseId,
                CourseName = ce.Course.CourseName,
                Description = ce.Course.Description
            }).ToList();

            return courses;
        }

        private bool StudentExists(int id) {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
