using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses() {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id) {
            var course = await _context.Courses.FindAsync(id);

            if (course == null) {
                return NotFound();
            }

            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDTO courseDto) {
            var course = new Course {
                CourseName = courseDto.CourseName,
                Description = courseDto.Description
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseUpdateDTO courseDto) {
            if (!CourseExists(id)) {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);

            if (course == null) {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(courseDto.CourseName)) {
                course.CourseName = courseDto.CourseName;
            }
            if (!string.IsNullOrEmpty(courseDto.Description)) {
                course.Description = courseDto.Description;
            }

            _context.Entry(course).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CourseExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id) {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetCourseStudents(int id) {
            var course = await _context.Courses
                .Include(c => c.CourseEnrollments)
                .ThenInclude(ce => ce.Student)
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null) {
                return NotFound();
            }

            var students = course.CourseEnrollments.Select(ce => new StudentDTO {
                StudentId = ce.Student.StudentId,
                FirstName = ce.Student.FirstName,
                LastName = ce.Student.LastName,
                DateOfBirth = ce.Student.DateOfBirth,
                Email = ce.Student.Email,
                Phone = ce.Student.Phone
            }).ToList();

            return students;
        }

        [HttpGet("{id}/teachers")]
        public async Task<ActionResult<IEnumerable<TeacherCreateDTO>>> GetCourseTeachers(int id) {
            var course = await _context.Courses
                .Include(c => c.TeacherCourses)
                .ThenInclude(tc => tc.Teacher)
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null) {
                return NotFound();
            }

            var teachers = course.TeacherCourses.Select(ce => new TeacherCreateDTO {
                FirstName = ce.Teacher.FirstName,
                LastName = ce.Teacher.LastName
            }).ToList();

            return teachers;
        }

        private bool CourseExists(int id) {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
