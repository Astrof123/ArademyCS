using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers {



    [Route("api/[controller]")]
    [ApiController]
    public class TeacherCoursesController : ControllerBase {
        private readonly AppDbContext _context;

        public TeacherCoursesController(AppDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TeacherCourseResponseDTO>> PostTeacherCourse(TeacherCourseCreateDTO teacherCourseDto) {
            var teacher = await _context.Teachers.FindAsync(teacherCourseDto.TeacherId);
            var course = await _context.Courses.FindAsync(teacherCourseDto.CourseId);

            if (teacher == null || course == null) {
                return BadRequest("Invalid TeacherId or CourseId");
            }

            var teacherCourse = new TeacherCourse {
                TeacherId = teacherCourseDto.TeacherId,
                CourseId = teacherCourseDto.CourseId
            };

            _context.TeacherCourses.Add(teacherCourse);
            await _context.SaveChangesAsync();

            var responseDto = new TeacherCourseResponseDTO {
                TeacherCourseId = teacherCourse.TeacherCourseId,
                TeacherId = teacherCourse.TeacherId,
                CourseId = teacherCourse.CourseId
            };

            return Ok(responseDto);
        }

        [HttpDelete("{teacherId}/{courseId}")]
        public async Task<IActionResult> DeleteTeacherCourse(int teacherId, int courseId) {
            var teacherCourse = await _context.TeacherCourses
                .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);


            if (teacherCourse == null) {
                return NotFound();
            }

            _context.TeacherCourses.Remove(teacherCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
