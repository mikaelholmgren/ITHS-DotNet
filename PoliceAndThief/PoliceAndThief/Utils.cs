using System;

namespace PoliceAndThief
{
    public class Utils
    {
        static private Random rnd = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static void PrintEvent(string v)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(v);
            System.Threading.Thread.Sleep(3000);
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(BlankLine());

        }
        public static void PrintStatusLine(string text)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(BlankLine());
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(text);

        }
        static string BlankLine()
        {
            // To be used to clear out a line based on the console width.
            string s = "";
            for (int i = 0; i < Console.WindowWidth -1; i++)
            {
                s += ' ';
            }
            return s;
        }
        public static void PrintPrison()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 9);
            var prisoners = Prison.GetCurrentPrisoners();
            Console.WriteLine($"Fängelset ({prisoners.Count} fångar)");
            Console.SetCursorPosition(0, Console.WindowHeight - 8);
            for (int j = 0; j < 7; j++)
            {
                
                Console.WriteLine(BlankLine());
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 8);
            int i = 1;
            DateTime now = DateTime.Now;
            foreach (var item in prisoners)
            {
                TimeSpan pTime = now - item.StartTime;
                Console.WriteLine($"Fånge {i}: suttit i: {pTime.Seconds} sekunder");
                i++;
            }
        }
    }
}