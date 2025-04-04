using Lab2_1.Models;
using System;

namespace Lab2_1 {
    internal class Program {
        static void Main(string[] args) {
            using (AppContext context = new AppContext()) {
                var course1 = new Course { Title = "C# Advanced", Duration = 60, Description = "Продвинутый курс по C#" };
                context.Courses.Add(course1);
                context.SaveChanges();
                Console.WriteLine($"Добавлен курс с Id = {course1.Id}");

                var courseFromDb = context.Courses.FirstOrDefault(c => c.Title == "C# Advanced");
                if (courseFromDb != null) {
                    Console.WriteLine($"Найден курс: {courseFromDb.Title}, Длительность: {courseFromDb.Duration}");
                }

                if (courseFromDb != null) {
                    courseFromDb.Duration = 70;
                    courseFromDb.Description = "Обновленное описание";
                    context.SaveChanges();
                    Console.WriteLine("Курс обновлен");
                    Console.WriteLine($"Найден курс: {courseFromDb.Title}, Длительность: {courseFromDb.Duration}");
                }
            }
        }
    }
}
