using System;

namespace PoliceAndThief
{
    public class Stats
    {
        public static int TotalRobs { get; private set; }
        public static int TotalCatchedThief { get; private set; }
        static Stats()
        {
            TotalRobs = 0;
            TotalCatchedThief = 0;
        }
       public static string PrintStatistics()
        {
            return $"Antal medborgarrån: {TotalRobs}, Polisingripanden: {TotalCatchedThief}";
        }
       public static void AddRob()
        {
            TotalRobs += 1;
        }
       public static void AddCatch()
        {
            TotalCatchedThief += 1;
        }
    }
}