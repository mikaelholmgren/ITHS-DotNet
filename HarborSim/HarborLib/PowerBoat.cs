using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    public class PowerBoat : Boat
    {
        public int HorsePower { get; }
        public PowerBoat() : base(BoatType.PowerBoat,
            $"M-{Utils.GetRandomId()}",
            "Motorbåt",
            Utils.Rnd.Next(200, 3000 + 1),
            Utils.Rnd.Next(1, 60 + 1),
            Utils.GetNumPositionsForBoatType(BoatType.PowerBoat), 3)
        {
            // In this default constructor we set predefined values and random values for the rest

            HorsePower = Utils.Rnd.Next(10, 1000 + 1);
        }
        public PowerBoat(string id, int weight, int maxSpeed, int horsePower, int day) : base(BoatType.PowerBoat,
    id,
    "Motorbåt",
    weight,
    maxSpeed,
    Utils.GetNumPositionsForBoatType(BoatType.PowerBoat), 3, day)
        {
            // In this default constructor we set predefined values and random values for the rest

            HorsePower = horsePower;
        }

        public override string GetAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)Type).Append(';');
            sb.Append(WharfNumber).Append(';');
            sb.Append(Identity).Append(';');
            sb.Append(Weight).Append(';');
            sb.Append(MaxSpeed).Append(';');
            sb.Append(HorsePower).Append(';');
            sb.Append(CurrentDay);
            return sb.ToString();
        }

        public override string GetUniqueProperty()
        {
            return $"Antal Hästkrafter: {HorsePower}";
        }

    }
}
