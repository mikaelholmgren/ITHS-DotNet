using System;
using System.Collections.Generic;
using System.Text;

namespace HarborLib
{
    static public class HarborUtils
    {
        static public void CreateRandomBoats(int num)
        {
            List<Boat> bl = new List<Boat>();

            for (int i = 0; i < num; i++)
            {
                int bn = Utils.Rnd.Next(1, 5 + 1);
                BoatType bt = (BoatType)bn;
                switch (bt)
                {
                    case BoatType.RowingBoat:
                        var rb = new RowingBoat();
                        bl.Add(rb);
                        break;
                    case BoatType.PowerBoat:
                        var pb = new PowerBoat();
                        bl.Add(pb);

                        break;
                    case BoatType.SailBoat:
                        var sb = new SailBoat();
                        bl.Add(sb);

                        break;
                    case BoatType.Catamaran:
                        var ca = new Catamaran();
                        bl.Add(ca);

                        break;
                    case BoatType.CargoBoat:
                        var cb = new CargoBoat();
                        bl.Add(cb);

                        break;
                    default:
                        break;
                }
            }
            foreach (var boat in bl)
            {
                boat.Arrive();
            }

        }

    }
}
