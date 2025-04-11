using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase {
        private readonly AppDbContext _context;

        public TeachersController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers() {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id) {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null) {
                return NotFound();
            }

            return teacher;
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherCreateDTO teacherDto) {
            var teacher = new Teacher {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherUpdateDTO teacherDto) {
            if (!TeacherExists(id)) {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null) {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(teacherDto.FirstName)) {
                teacher.FirstName = teacherDto.FirstName;
            }
            if (!string.IsNullOrEmpty(teacherDto.LastName)) {
                teacher.LastName = teacherDto.LastName;
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!TeacherExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id) {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetTeacherCourses(int id) {
            var teacher = await _context.Teachers
                .Include(t => t.TeacherCourses)
                .ThenInclude(tc => tc.Course)
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null) {
                return NotFound();
            }

            var courses = teacher.TeacherCourses.Select(tc => tc.Course).ToList();
            return courses;
        }

        private bool TeacherExists(int id) {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
