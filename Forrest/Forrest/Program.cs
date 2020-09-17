using System;
using System.Collections.Generic;

namespace Forrest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            List<Animal> forrest = new List<Animal>();
            forrest.Add(CreateAnimalWithProps("Fladdermus", true, true));
            forrest.Add(CreateAnimalWithProps("Varg", true, false));
            forrest.Add(CreateAnimalWithProps("Örn", false, true));
            forrest.Add(CreateAnimalWithProps("häst", false, false, "galopperar omkring och har kul"));
            while (running) 
            {
                Console.Write("Välkommen till skogen! Tryck d för dag eller n för natt");
                char key = Console.ReadKey(true).KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case 'd':
                        foreach (var animal in forrest)
                        {
                            Console.WriteLine(animal.GetActionMessage(true));
                            

                        }
                        break;
                    case 'n':
                        foreach (var animal in forrest)
                        {
                            Console.WriteLine(animal.GetActionMessage(false));


                        }
                        break;
                    case 'a':
                        running = false;
                        Console.WriteLine("Du lämnar nu skogen, hejdå!");
                        break;
                    default:
                        break;
                }
            }
        }

        private static Animal CreateAnimalWithProps(string name, bool nocturnal, bool wings, string custom = null)
        {
            Animal animal = new Animal();
            animal.Name = name;
            animal.Nocturnal = nocturnal;
            animal.HaveWings = wings;
            if (custom != null)
                animal.CustomAction = $"{name}en " + custom;
            return animal;
        }
    }
}
