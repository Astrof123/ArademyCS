using System;
using System.Threading;

public class Lab1_3 {
    private static double sharedValue = 0.5;
    private static readonly object lockObject = new object();
    private static bool cosTurn = true;

    public static void Main(string[] args) {
        Thread cosThread = new Thread(Calculatecos);
        cosThread.Name = "CosThread";

        Thread arccosThread = new Thread(CalculateArccos);
        arccosThread.Name = "ArccosThread";

        cosThread.Start();
        arccosThread.Start();

        cosThread.Join();
        arccosThread.Join();

        Console.WriteLine("Основной поток завершен.");
    }

    private static void Calculatecos() {
        while (true)
        {
            lock (lockObject) {
                while (!cosTurn) {
                    Monitor.Wait(lockObject);
                }

                Console.WriteLine($"Поток {Thread.CurrentThread.Name}: Исходное значение = {sharedValue}");
                sharedValue = Math.Cos(sharedValue);
                Console.WriteLine($"Поток {Thread.CurrentThread.Name}: Cos({sharedValue}) = {sharedValue}");

                cosTurn = false;
                Monitor.Pulse(lockObject);
            }
            Thread.Sleep(100);
        }
    }

    private static void CalculateArccos() {
        while (true)
        {
            lock (lockObject) { 
                while (cosTurn) {
                    Monitor.Wait(lockObject);
                }

                Console.WriteLine($"Поток {Thread.CurrentThread.Name}: Исходное значение = {sharedValue}");
                if (sharedValue >= -1 && sharedValue <= 1) {
                    sharedValue = Math.Acos(sharedValue);
                    Console.WriteLine($"Поток {Thread.CurrentThread.Name}: Arccos({sharedValue}) = {sharedValue}");
                }
                else {
                    Console.WriteLine($"Поток {Thread.CurrentThread.Name}: Значение {sharedValue} вне допустимого диапазона для Arccos.  Сбрасываем значение.");
                    sharedValue = 0.5;
                }

                cosTurn = true;
                Monitor.Pulse(lockObject);
            }
            Thread.Sleep(100);
        }
    }
}