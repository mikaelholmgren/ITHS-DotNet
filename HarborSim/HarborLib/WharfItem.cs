namespace HarborLib
{
    public class WharfItem
    {
        // This is used for displaying the list of wharf and boats
        public string WharfNumber { get; set; } // String because we want to indicate when a boat takes multiple positions
        public string BoatId { get; set; }
        public string BoatWeight { get; set; }
        public string BoatMaxSpeed { get; set; }
        public string BoatUniqueProp { get; set; }

        public string BoatType { get; set; }
        public override string ToString()
        { // This is mainly for accessibility reason with the Listview that renders this
            return $"{WharfNumber} {BoatType} {BoatId} {BoatWeight} {BoatMaxSpeed} {BoatUniqueProp}";
        }
    }
}
