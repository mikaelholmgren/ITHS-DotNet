using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    public class RowingBoat : Boat
    {
        public int MaxPassengers { get; }
        public RowingBoat() : base(BoatType.RowingBoat,
            $"R-{Utils.GetRandomId()}",
            "Roddbåt",
            Utils.Rnd.Next(100, 300 + 1),
            Utils.Rnd.Next(1, 3 + 1),
            Utils.GetNumPositionsForBoatType(BoatType.RowingBoat), 1)
        {
            // In this default constructor we set predefined values and random values for the rest
            
            MaxPassengers = Utils.Rnd.Next(1, 6 + 1);
        }
        public RowingBoat(string id, int weight, int maxSpeed, int passengers, int day) : base(BoatType.RowingBoat,
            id,
            "Roddbåt",
            weight,
            maxSpeed,
            Utils.GetNumPositionsForBoatType(BoatType.RowingBoat), 1, day)
        {
            // In this default constructor we set predefined values and random values for the rest

            MaxPassengers = passengers;
        }
        public override string GetAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)Type).Append(';');
            sb.Append(WharfNumber).Append(';');
            sb.Append(Identity).Append(';');
            sb.Append(Weight).Append(';');
            sb.Append(MaxSpeed).Append(';');
            sb.Append(MaxPassengers).Append(';');
            sb.Append(CurrentDay);
            return sb.ToString();
        }

        public override string GetUniqueProperty()
        {
            return $"Max passagerare: {MaxPassengers}";
        }
    }
}
