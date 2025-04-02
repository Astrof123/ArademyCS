using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_5 {
    public class Grade {
        public string StudentId { get; set; }
        public int Score { get; set; }
    }

    public class Student {
        public string StudentId { get; set; }
        public string Name { get; set; }
    }

    public class GradeCalculator {
        public Dictionary<string, double> CalculateAverageGradeSingleThread(List<Grade> grades, List<Student> students) {
            var studentAverages = new Dictionary<string, double>();
            foreach (var student in students) {
                var studentGrades = grades.Where(g => g.StudentId == student.StudentId).ToList();
                if (studentGrades.Any()) {
                    studentAverages[student.StudentId] = studentGrades.Average(g => g.Score);
                }
                else {
                    studentAverages[student.StudentId] = 0;
                }
            }
            return studentAverages;
        }

        public async Task<Dictionary<string, double>> CalculateAverageGradeMultiThreadAsync(List<Grade> grades, List<Student> students) {
            var studentAverages = new Dictionary<string, double>();
            var tasks = students.Select(async student => {
                await Task.Delay(5);

                var studentGrades = grades.Where(g => g.StudentId == student.StudentId).ToList();
                if (studentGrades.Any()) {
                    lock (studentAverages) {
                        studentAverages[student.StudentId] = studentGrades.Average(g => g.Score);
                    }
                }
                else {
                    lock (studentAverages) {
                        studentAverages[student.StudentId] = 0;
                    }
                }
            });

            await Task.WhenAll(tasks);
            return studentAverages;
        }
    }

    class Program {
        static async Task Main(string[] args) {
            Student student1 = new Student { StudentId = "S1", Name = "Алиса" };
            Student student2 = new Student { StudentId = "S2", Name = "Борис" };
            List<Student> students = new List<Student> { student1, student2 };

            Grade grade1 = new Grade { StudentId = "S1", Score = 90 };
            Grade grade2 = new Grade { StudentId = "S1", Score = 85 };
            Grade grade3 = new Grade { StudentId = "S2", Score = 75 };

            List<Grade> grades = new List<Grade> { grade1, grade2, grade3 };
            var gradeCalculator = new GradeCalculator();


            var stopwatchSingleThread = Stopwatch.StartNew();
            var singleThreadResults = gradeCalculator.CalculateAverageGradeSingleThread(grades, students);
            stopwatchSingleThread.Stop();

            Console.WriteLine("Однопоточный метод:");
            foreach (var result in singleThreadResults) {
                Console.WriteLine($"Студент {result.Key}: Средний балл = {result.Value}");
            }
            Console.WriteLine($"Время выполнения: {stopwatchSingleThread.ElapsedMilliseconds} мс");
            Console.WriteLine();


            var stopwatchMultiThread = Stopwatch.StartNew();
            var multiThreadResults = await gradeCalculator.CalculateAverageGradeMultiThreadAsync(grades, students);
            stopwatchMultiThread.Stop();

            Console.WriteLine("Многопоточный метод:");
            foreach (var result in multiThreadResults) {
                Console.WriteLine($"Студент {result.Key}: Средний балл = {result.Value}");
            }
            Console.WriteLine($"Время выполнения: {stopwatchMultiThread.ElapsedMilliseconds} мс");
            Console.WriteLine();


            bool areEqual = singleThreadResults.SequenceEqual(multiThreadResults);
            Console.WriteLine($"Результаты однопоточного и многопоточного методов совпадают: {areEqual}");
        }
    }
}