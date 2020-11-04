using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    public class CargoBoat : Boat
    {
        public int NumContainers { get; }
        public CargoBoat() : base(BoatType.CargoBoat,
            $"L-{Utils.GetRandomId()}",
            "Lastfartyg",
            Utils.Rnd.Next(3000, 20000 + 1),
            Utils.Rnd.Next(1, 20 + 1),
            Utils.GetNumPositionsForBoatType(BoatType.CargoBoat), 6)
        {
            // In this default constructor we set predefined values and random values for the rest

            NumContainers = Utils.Rnd.Next(0, 500 + 1);
        }
        public CargoBoat(string id, int weight, int maxSpeed, int containers, int day) : base(BoatType.CargoBoat,
            id,
            "Lastfartyg",
            weight,
            maxSpeed,
            Utils.GetNumPositionsForBoatType(BoatType.CargoBoat), 6, day)
        {
            // In this default constructor we set predefined values and random values for the rest

            NumContainers = containers;
        }


        public override string GetUniqueProperty()
        {
            return $"Containers: {NumContainers}";
        }

        public override string GetAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)Type).Append(';');
            sb.Append(WharfNumber).Append(';');
            sb.Append(Identity).Append(';');
            sb.Append(Weight).Append(';');
            sb.Append(MaxSpeed).Append(';');
            sb.Append(NumContainers).Append(';');
            sb.Append(CurrentDay);
            return sb.ToString();
        }
    }
}
