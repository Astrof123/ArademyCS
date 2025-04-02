namespace Lab1_2 {
    internal class Program {
        static Thread? thread1;
        static void Main(string[] args) {
            thread1 = new Thread(() => DoWork1("Поток 1", 100));
            Thread thread2 = new Thread(() => DoWork2("Поток 2", 100));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("\nЭксперимент: ");
            thread1 = new Thread(() => DoWork1("Поток 1", 100));
            thread2 = new Thread(() => DoWork2("Поток 2", 100));

            thread1.Start();
            Thread.Sleep(1000); 
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static void DoWork1(string threadName, int iterations) {
            for (int i = 0; i < iterations; i++) {
                Console.WriteLine($"{threadName}: итерация {i}");
                Thread.Sleep(40); 
            }
        }

        static void DoWork2(string threadName, int iterations) {
            Console.WriteLine($"{threadName} ждет своей очереди");
            thread1.Join();
            for (int i = 0; i < iterations; i++) {
                Console.WriteLine($"{threadName}: итерация {i}");
                Thread.Sleep(40);
            }
        }
    }
}
