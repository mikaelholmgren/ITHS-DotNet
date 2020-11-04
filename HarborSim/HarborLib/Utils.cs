using System;
using System.Collections.Generic;

namespace HarborLib
{
    internal class Utils
    {
        internal static Random Rnd = new Random();
        internal static string GetRandomId()
        {
            string retStr = "";
            for (int i = 0; i < 3; i++)
            {
                char c = (char)Rnd.Next(65, 90 + 1); // characters between A..Z
                retStr += c.ToString();
            }
            return retStr;
        }

        internal static int GetNumPositionsForBoatType(BoatType type)
        {
            int retval = 0;
            switch (type)
            {
                case BoatType.RowingBoat:
                    retval = 1; // Not exactly true since it takes just 0.5, but we handle that elsewhere
                    break;
                case BoatType.PowerBoat:
                    retval = 1;
                    break;
                case BoatType.SailBoat:
                    retval = 2;
                    break;
                case BoatType.Catamaran:
                    retval = 3;
                    break;
                case BoatType.CargoBoat:
                    retval = 4;
                    break;
                default:
                    break;
            }
            return retval;
        }
    }
}