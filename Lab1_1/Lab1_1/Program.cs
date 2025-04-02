namespace Lab1_1 {
    class Program {
        private static void Main(string[] args) {

            Thread myThread = new Thread(Print);
            Thread myThread2 = new Thread(Print);

            myThread.Start(new Range(5, 12));
            myThread2.Start(new Range(20, 34));

            void Print(object? obj) {
                if (obj is Range range) {
                    for (int i = range.startNum; i <= range.endNum; i++) {
                        Console.WriteLine(i);
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
    record class Range(int startNum, int endNum);
}