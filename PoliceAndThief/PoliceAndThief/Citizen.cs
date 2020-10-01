using System;
using System.Collections.Generic;

namespace PoliceAndThief
{
    public class Citizen : Person
    {
        public List<Item> Belongings { get; set; }
        public Citizen()
        {
            Belongings = new List<Item>();
            CreateDefaultInventory();
        }

        private void CreateDefaultInventory()
        {
            Belongings.Add(new Item("Nycklar"));
            Belongings.Add(new Item("Mobiltelefon"));
            Belongings.Add(new Item("Pengar"));
            Belongings.Add(new Item("Klocka"));
        }

        public override char Display()
        {
            return 'M';
        }
    }
}
