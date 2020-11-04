namespace HarborLib
{
    public class HarborStats
    {
        public int NumRowingBoats { get; internal set; }
        public int NumPowerBoats { get; internal set; }
        public int NumSailBoats { get; internal set; }
        public int NumCatamarans { get; internal set; }
        public int NumCargoBoats { get; internal set; }
        public int TotalWeight { get; internal set; }
        public int AverageMaxSpeedKMPH { get; internal set; }
        public int FreeSlots { get; internal set; }
        public int DeniedBoats { get; internal set; }
    }
}