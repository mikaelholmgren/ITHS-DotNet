using System.Collections.Generic;

namespace HarborLib
{
    public class WharfPoint
    {
        public BoatType Type { get; set; }
        public List<Boat> Boats { get; set; } // this is only because of the rowingboats that can be 2 i a position
        public int WharfNumber { get; }

        public WharfPoint(int number)
        {
            Type = BoatType.None;
            Boats = new List<Boat>();
            WharfNumber = number;
        }
    }
}