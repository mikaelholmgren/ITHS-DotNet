using System;

namespace PoliceAndThief
{
    public class PrisonItem
    {
        public Person Prisoner { get; set; }
        public DateTime StartTime { get; set; }
        public PrisonItem(Person p)
        {
            Prisoner = p;
            StartTime = DateTime.Now;
        }

    }
}