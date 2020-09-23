using System;
using System.Collections.Generic;
using System.Linq;

namespace E8_1_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu = "Visa kategori:\n [F]aktaböcker\n[B]arnböcker\n[U]nderhållning\n<Escape> för att avsluta\nVälj ett filter, eller <mellanslag> för att visa alla: ";
            bool running = true;
            List<Book> library = createLibrary();
            while (running)
            {
                
                var (filter, shouldExit) = ShowMenu(menu, new char[] { 'f', 'b', 'u', ' ' });
                if (!shouldExit)
                    PrintLibrary(library, filter);
                else running = false;
            }
            Console.WriteLine("Goodbye!");
        }

        private static (char filter, bool shouldExit) ShowMenu(string menuText, char[] keyList)
        {
            bool exitFlag = false;
            ConsoleKeyInfo ki;
            Console.Clear();
            Console.Write(menuText);
            
            while (true)
            {
                ki = Console.ReadKey(true);
                if (keyList.Contains(ki.KeyChar)) break;
                if (ki.Key == ConsoleKey.Escape)
                {
                    exitFlag = true;
                    break;
                }
            }
            Console.WriteLine();
            return (ki.KeyChar, exitFlag);
        }

        private static void PrintLibrary(List<Book> library, char filter)
        {
            int displayed = 0;
            Console.Clear();
            Console.WriteLine($"Listar kategori: {printChoosenFilter(filter)}");
            foreach (Book b in library)
            {
                if (filter == 'f' && !(b is FactsBook)) continue;
                else if (filter == 'b' && !(b is ChildrensBook)) continue;
                else if (filter == 'u' && !(b is EntertainmentBook)) continue;
                // If we got here, the current book is in the choosen category, or we are listing all categories.
                if (displayed++ == 3)
                {
                    waitForSpace("Tryck mellanslag för att fortsätta listningen...");
                    displayed = 0;
                }

                printEntry(b);
            }
            waitForSpace("Tryck mellanslag för att återgå till menyn...");
        }

        private static string printChoosenFilter(char filter)
        {
            switch (filter)
            {
                case 'f': return "Fakta böcker";
                    break;
                case 'b': return "Barnböcker";
                    break;
                case 'u': return "Underhållning";
                    break;
                case ' ': return "Alla";
                    break;
                default:
                    return "Okänd";
                    break;
            }
        }

        private static void waitForSpace(string v)
        {
            Console.Write(v);
            do
            {

            } while (Console.ReadKey(true).KeyChar != ' ');
            Console.Clear();
        }

        private static void printEntry(Book b)
        {
            Console.WriteLine($"Kategori: {categoryOf(b)}");
            Console.WriteLine($"Titel: {b.Title}");
            Console.WriteLine($"Författare: {b.Author}");
            Console.WriteLine($"Antal sidor: {b.NumPages}");
            if (b is FactsBook)
            {
                FactsBook fb = (FactsBook)b;
                Console.WriteLine($"Ämne: {fb.Topic}");
                Console.WriteLine($"Svårighetsgrad: {fb.Difficulty}");
            }
            else if (b is ChildrensBook)
            {
                ChildrensBook cb = (ChildrensBook)b;
                Console.WriteLine($"Målgrupp: {(cb.YoungAdults ? "Ungdom" : "Barn")}");
                Console.WriteLine($"Bilderbok: {(cb.PictureBook ? "Ja" : "Nej")}");
            }
            else if (b is EntertainmentBook)
            {
                EntertainmentBook eb = (EntertainmentBook)b;
                Console.WriteLine($"Typ: {eb.Type}");
                Console.WriteLine($"Samling: {(eb.Anthology ? "Ja" : "Nej")}");
            }
            else
            {
                Console.WriteLine("Okänd kategori!");
            }

        }

        private static string categoryOf(Book b)
        {
            switch (b.GetType().Name)
            {
                case "FactsBook": return "Faktaböcker";
                    break;
                case "ChildrensBook": return "Barnböcker";
                    break;
                case "EntertainmentBook": return "Underhållning";
                    break;
                default:
                    return "Okänd kategori";
                    break;
            }
        }

        private static List<Book> createLibrary()
        {
            List<Book> lib = new List<Book>();
            lib.Add(new FactsBook("Författaren förf", "Fakta om guldfiskar", 400, "Fiskar", 1));
            lib.Add(new FactsBook("Författaren förf", "Fakta om hundar", 400, "Hundar", 1));
            lib.Add(new ChildrensBook("Olle Ollesson", "Griseknoens äventyr", 40, false, true));
            lib.Add(new ChildrensBook("Olle Ollesson", "Livets gåtor i ungdomsåren", 400, true, false));
            lib.Add(new EntertainmentBook ("Ulla Ulvsdotter", "Robert Linds irfärder", 400, "Fantasy", false));
            lib.Add(new EntertainmentBook("Ulla Ulvsdotter", "Vogonsk Poesi", 400, "Fiction", true));
            return lib;
        }
    }
}
