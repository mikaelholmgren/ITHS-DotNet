using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    public class Catamaran : Boat
    {
        public int NumBeds { get; }
        public Catamaran() : base(BoatType.Catamaran,
            $"K-{Utils.GetRandomId()}",
            "Katamaran",
            Utils.Rnd.Next(1200, 8000 + 1),
            Utils.Rnd.Next(1, 12 + 1),
            Utils.GetNumPositionsForBoatType(BoatType.Catamaran), 3)
        {
            // In this default constructor we set predefined values and random values for the rest

            NumBeds = Utils.Rnd.Next(1, 4 + 1);
        }
        public Catamaran(string id, int weight, int maxSpeed, int beds, int day) : base(BoatType.Catamaran,
            id,
            "Katamaran",
            weight,
            maxSpeed,
            Utils.GetNumPositionsForBoatType(BoatType.Catamaran), 3, day)
        {
            // In this default constructor we set predefined values and random values for the rest

            NumBeds = beds;
        }

        public override string GetUniqueProperty()
        {
            return $"Antal bäddplatser: {NumBeds}";
        }
        public override string GetAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)Type).Append(';');
            sb.Append(WharfNumber).Append(';');
            sb.Append(Identity).Append(';');
            sb.Append(Weight).Append(';');
            sb.Append(MaxSpeed).Append(';');
            sb.Append(NumBeds).Append(';');
            sb.Append(CurrentDay);
            return sb.ToString();
        }

    }
}
