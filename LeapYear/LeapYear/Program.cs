using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace LeapYear
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Ange ett årtal: ");
            //int year = int.Parse(Console.ReadLine());
            //if (IsLeapYear(year)) Console.WriteLine($"{year} är ett skottår");
            //else Console.WriteLine($"{year} är inte ett skottår");
            Console.WriteLine("Ange årtal separerade med mellanslag: ");
            string input = Console.ReadLine();
            
            if (IsFile(input))
            {
                Console.WriteLine("Läser från fil.");
                input = ReadFile(input);
            }

            string[] list = input.Split();
            PrintYears(list);
        }

        private static bool IsFile(string input)
        {
            if (File.Exists(input)) return true;
            else return false;
        }

        private static string ReadFile(string input)
        {
            using (var s = File.OpenText(input))
            {
                return s.ReadToEnd();
            }
        }
        private static void PrintYears(string[] list)
        {
            int[] years = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                years[i] = int.Parse(list[i]);
            }
            foreach (var item in years)
            {
                if (IsLeapYear(item)) Console.Write($"[{item}] ");
                else Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        private static bool IsLeapYear(int year)
        {
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 ==0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }
    }
}
