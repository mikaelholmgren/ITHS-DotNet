using System;
using System.Collections.Generic;

namespace PoliceAndThief
{
    class Program
    {
        static int consoleWidth = 100;
        static int consoleHeight = 35;
        static Gameboard CurrentBoard;
        static List<Person> PeopleInCity;
        private static int numCitizens = Utils.GetRandomNumber(20, 41);
        private static int numPolices = Utils.GetRandomNumber(10, 20);
        private static int numThief = Utils.GetRandomNumber(10, 20);

        static void Main(string[] args)
        {
            Console.SetWindowSize(consoleWidth, consoleHeight);
            // Trying to get app to work in a normal console outside of debug
            // This needs more investigation. Works fine in debug console though
            Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.Clear();
            Console.WriteLine($"Denna gång har vi {numCitizens} medborgare, {numThief} tjuvar och {numPolices} poliser.");
            Console.Write("Tryck en tangent för att starta (Escape avslutar pågående spel efter start)...");
            Console.ReadKey(true);
            CurrentBoard = new Gameboard(GameSettings.BoardSizeY, GameSettings.BoardSizeX);
            PeopleInCity = new List<Person>();
            bool running = true;
            CreatePeople();
            Console.Clear();
            while (running)
            {
                UpdatePeopleOnBoard();
                CurrentBoard.Draw();
                CurrentBoard.CheckForCollitions();
                Prison.TickAndRelease();
                Utils.PrintPrison();
                Utils.PrintStatusLine(Stats.PrintStatistics() + Prison.GetPrisonStatus());
                MovePeople();
                running = CheckKeyPresses();
            }
            Console.Clear();
            Console.WriteLine("Bye");
        }


        private static bool CheckKeyPresses()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                return key != ConsoleKey.Escape;
            }
            else return true;
        }


        private static void MovePeople()
        {
            foreach (Person person in PeopleInCity)
            {
                if (Prison.CheckIfInPrison(person)) continue;
                person.Move();
            }
        }

        private static void UpdatePeopleOnBoard()
        {
            CurrentBoard.Clear();
            foreach (Person person in PeopleInCity)
            {
                if (Prison.CheckIfInPrison(person)) continue;
                var cell = CurrentBoard.GetCell(person.Y, person.X);
                cell.People.Add(person);
            }
        }

        private static void CreatePeople()
        {
            for (int i = 0; i < numCitizens; i++)
                PeopleInCity.Add(new Citizen());
            for (int i = 0; i < numPolices; i++)
                PeopleInCity.Add(new Police());
            for (int i = 0; i < numThief; i++)
                PeopleInCity.Add(new Thief());

        }
    }
}
