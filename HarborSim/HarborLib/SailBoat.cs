using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    public class SailBoat : Boat
    {
        public int BoatLength { get; }
        public SailBoat() : base(BoatType.SailBoat,
            $"S-{Utils.GetRandomId()}",
            "Segelbåt",
            Utils.Rnd.Next(800, 6000 + 1),
            Utils.Rnd.Next(1, 12 + 1),
            Utils.GetNumPositionsForBoatType(BoatType.SailBoat), 4)
        {
            // In this default constructor we set predefined values and random values for the rest

            BoatLength = Utils.Rnd.Next(10, 60 + 1);
        }
        public SailBoat(string id, int weight, int maxSpeed, int boatLength, int day) : base(BoatType.SailBoat,
    id,
    "Segelbåt",
    weight,
    maxSpeed,
    Utils.GetNumPositionsForBoatType(BoatType.SailBoat), 4, day)
        {
            // In this default constructor we set predefined values and random values for the rest

            BoatLength = boatLength;
        }

        public override string GetAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)Type).Append(';');
            sb.Append(WharfNumber).Append(';');
            sb.Append(Identity).Append(';');
            sb.Append(Weight).Append(';');
            sb.Append(MaxSpeed).Append(';');
            sb.Append(BoatLength).Append(';');
            sb.Append(CurrentDay);
            return sb.ToString();
        }

        public override string GetUniqueProperty()
        {
            return $"Båtlängd: {BoatLength} fot";
        }

    }
}
