using Microsoft.EntityFrameworkCore;

namespace Lab2_2 {
    public class Program {
        static void Main(string[] args) {
            using (var context = new UniversityContext()) {
                // Eager Loading
                Console.WriteLine("Eager Loading:");
                var coursesEager = context.Courses
                    .Include(c => c.Teachers)
                    .Include(c => c.Students)
                    .ToList();

                foreach (var course in coursesEager) {
                    Console.WriteLine($"Course: {course.Title}");
                    Console.WriteLine("  Teachers:");
                    foreach (var teacher in course.Teachers) {
                        Console.WriteLine($"    - {teacher.Name}");
                    }
                    Console.WriteLine("  Students:");
                    foreach (var student in course.Students) {
                        Console.WriteLine($"    - {student.Name}");
                    }
                }

                // Explicit Loading
                Console.WriteLine("\nExplicit Loading:");
                var courseExplicit = context.Courses.First();
                if (courseExplicit != null) {
                    context.Entry(courseExplicit).Collection(c => c.Teachers).Load();
                    context.Entry(courseExplicit).Collection(c => c.Students).Load();

                    Console.WriteLine($"Course: {courseExplicit.Title}");
                    Console.WriteLine("  Teachers:");
                    foreach (var teacher in courseExplicit.Teachers) {
                        Console.WriteLine($"    - {teacher.Name}");
                    }
                    Console.WriteLine("  Students:");
                    foreach (var student in courseExplicit.Students) {
                        Console.WriteLine($"    - {student.Name}");
                    }
                }

                // Lazy Loading
                Console.WriteLine("\nLazy Loading:");
                var courseLazy = context.Courses.First();
                if (courseLazy != null) {
                    Console.WriteLine($"Course: {courseLazy.Title}");
                    Console.WriteLine("  Teachers:");
                    foreach (var teacher in courseLazy.Teachers) 
                    {
                        Console.WriteLine($"    - {teacher.Name}");
                    }
                    Console.WriteLine("  Students:");
                    foreach (var student in courseLazy.Students)
                    {
                        Console.WriteLine($"    - {student.Name}");
                    }
                }
            }
        }
    }
}
