using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace HarborLib
{
    static public class Harbor
    {
        static private List<Boat> currentBoats = new List<Boat>();
        static private List<Boat> deleteList = new List<Boat>();
        static private WharfPoint[] wharf1 = new WharfPoint[32];
        static private WharfPoint[] wharf2 = new WharfPoint[32];

        public static Action<string> EventLoggerAction { get; set; }
        public static bool DebugMode { get; private set; }

        static public int Day { get; private set; }
        public static bool AutomaticTimeSwitching { get; set; }


        private static int deniedBoats = 0;
        private static string dataFileName = "HarborData.csv";
        private static string pendingLogTxt = ""; // Used to catch the log entries that comes before the event handler for the log is setup
        public static int NumRandomBoats { get; set; }
        public static TimeSpan TimeBetweenDays { get; set; }

        static Harbor()
        {
            DebugMode = false;
            Day = 1;
            NumRandomBoats = 5;
            TimeBetweenDays = TimeSpan.FromSeconds(5);
            InitWharfs();
            LogEvent("Start of harbortime, its day 1");
        }

        private static void InitWharfs()
        {
            int wharfNum = 1;
            for (int i = 0; i < 32; i++)
            {
                wharf1[i] = new WharfPoint(wharfNum++);
            }
            for (int i = 0; i < 32; i++)
            {
                wharf2[i] = new WharfPoint(wharfNum++);
            }
        }

        static private void LogEvent(string text)
        {
            if (EventLoggerAction != null)
            {
                EventLoggerAction($"{pendingLogTxt}Day {Day}: {text}");
                if (!string.IsNullOrEmpty(pendingLogTxt)) pendingLogTxt = "";
            }
            else pendingLogTxt += $"Day {Day}: {text}\n";
        }

        static public void Tick()
        {
            Day++;
            LogEvent($"-------{NumRandomBoats} new boats-----------");
            foreach (Boat b in currentBoats)
            {
                b.Tick();
            }
            foreach (var boat in deleteList)
            {
                currentBoats.Remove(boat);
            }
            deleteList.Clear();

        }
        static public void SaveBoats()
        {
            if (currentBoats.Count == 0) return;
            StringBuilder sb = new StringBuilder();
            foreach (Boat boat in currentBoats)
            {
                sb.AppendLine(boat.GetAsCSV());
            }
            File.WriteAllText(dataFileName, sb.ToString());
        }
        static public bool LoadBoats()
        {
            if (!File.Exists(dataFileName)) return false;
            int count = 0;
            var lines = File.ReadAllLines(dataFileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                var chunks = line.Split(';');
                BoatType t = (BoatType)int.Parse(chunks[0]);
                switch (t)
                {
                    case BoatType.RowingBoat:
                        var rb = new RowingBoat(chunks[2], int.Parse(chunks[3]), int.Parse(chunks[4]), int.Parse(chunks[5]), int.Parse(chunks[6]));
                        rb.WharfNumber = int.Parse(chunks[1]);
                        RegisterBoat(rb, true);
                        count++;
                        break;
                    case BoatType.PowerBoat:
                        var pb = new PowerBoat(chunks[2], int.Parse(chunks[3]), int.Parse(chunks[4]), int.Parse(chunks[5]), int.Parse(chunks[6]));
                        pb.WharfNumber = int.Parse(chunks[1]);
                        RegisterBoat(pb, true);
                        count++;

                        break;
                    case BoatType.SailBoat:
                        var sb = new SailBoat(chunks[2], int.Parse(chunks[3]), int.Parse(chunks[4]), int.Parse(chunks[5]), int.Parse(chunks[6]));
                        sb.WharfNumber = int.Parse(chunks[1]);
                        RegisterBoat(sb, true);
                        count++;

                        break;
                    case BoatType.Catamaran:
                        var cb = new Catamaran(chunks[2], int.Parse(chunks[3]), int.Parse(chunks[4]), int.Parse(chunks[5]), int.Parse(chunks[6]));
                        cb.WharfNumber = int.Parse(chunks[1]);
                        RegisterBoat(cb, true);
                        count++;

                        break;
                    case BoatType.CargoBoat:
                        var cab = new CargoBoat(chunks[2], int.Parse(chunks[3]), int.Parse(chunks[4]), int.Parse(chunks[5]), int.Parse(chunks[6]));
                        cab.WharfNumber = int.Parse(chunks[1]);
                        RegisterBoat(cab, true);
                        count++;

                        break;
                    default:
                        break;
                }
            }
            if (count == 0) return false;
            LogEvent($"Loaded {count} boats from file");
            return true;
        }

        static public void GetWharfItems(int wharf, ObservableCollection<WharfItem> wfList)
        {
            wfList.Clear();
            int counter = 0; // We use a manual counter to be able to skip over items when a boat takes multiple once.
            var ourWharf = wharf == 2 ? wharf2 : wharf1;
            while (counter < 32)
            {
                var curPoint = ourWharf[counter];
                if (curPoint.Type == BoatType.None)
                {
                    WharfItem item = new WharfItem();
                    item.WharfNumber = curPoint.WharfNumber.ToString();
                    item.BoatType = "Ledig";
                    item.BoatId = "-";
                    item.BoatWeight = "-";
                    item.BoatMaxSpeed = "-";
                    item.BoatUniqueProp = "-";
                    wfList.Add(item);
                    counter++;
                }
                else if (curPoint.Type == BoatType.RowingBoat) // special case
                {
                    foreach (Boat b in curPoint.Boats)
                    {
                        WharfItem item = new WharfItem();
                        item.WharfNumber = curPoint.WharfNumber.ToString();
                        item.BoatType = b.BoatTypeName;
                        item.BoatId = b.Identity;
                        item.BoatWeight = b.Weight.ToString();
                        item.BoatMaxSpeed = ConvertToKMPH(b.MaxSpeed).ToString();
                        item.BoatUniqueProp = b.GetUniqueProperty();
                        wfList.Add(item);
                    }
                    counter++;
                }
                else
                {
                    Boat b = curPoint.Boats.FirstOrDefault();
                    WharfItem item = new WharfItem();
                    string wharfnumStr = GeneratePointString(curPoint.WharfNumber, b.NumPositions);
                    item.WharfNumber = wharfnumStr;
                    item.BoatType = b.BoatTypeName;
                    item.BoatId = b.Identity;
                    item.BoatWeight = b.Weight.ToString();
                    item.BoatMaxSpeed = ConvertToKMPH(b.MaxSpeed).ToString();
                    item.BoatUniqueProp = b.GetUniqueProperty();
                    wfList.Add(item);
                    counter += b.NumPositions;
                }
            }
            
        }

        private static int ConvertToKMPH(int speed)
        {
            return (int)Math.Round(speed * 1.852);
        }
        static public HarborStats GetStatistics()
        {
            HarborStats s = new HarborStats();
            s.NumRowingBoats = currentBoats.Count(c => c.Type == BoatType.RowingBoat);
            s.NumPowerBoats = currentBoats.Count(c => c.Type == BoatType.PowerBoat);
            s.NumSailBoats = currentBoats.Count(c => c.Type == BoatType.SailBoat);
            s.NumCatamarans = currentBoats.Count(c => c.Type == BoatType.Catamaran);
            s.NumCargoBoats = currentBoats.Count(c => c.Type == BoatType.CargoBoat);
            s.TotalWeight = currentBoats.Sum(su => su.Weight);
            int numBoats = currentBoats.Count;
            int totalMaxSpeed = currentBoats.Sum(su => su.MaxSpeed);
            s.AverageMaxSpeedKMPH = ConvertToKMPH(totalMaxSpeed / numBoats);
            s.FreeSlots = wharf1.Count(c => c.Type == BoatType.None) + wharf2.Count(c => c.Type == BoatType.None);
            s.DeniedBoats = deniedBoats;
            return s;
        }
        internal static int GetFreeWharfNumber(BoatType type)
        {
            int neededPositions = Utils.GetNumPositionsForBoatType(type);
            int retval = -1;
            if (type == BoatType.RowingBoat)
            {
                WharfPoint point = wharf1.Where(c => c.Type == BoatType.RowingBoat && c.Boats.Count == 1).FirstOrDefault();
                if (point != null)
                { // We did find a spot with just one rowingboat so we can use it
                    if (DebugMode) LogEvent($"Check availability for {type}, offering number {point.WharfNumber}");
                    return point.WharfNumber;
                }
                // look at the other wharf as well
                point = wharf2.Where(c => c.Type == BoatType.RowingBoat && c.Boats.Count == 1).FirstOrDefault();
                if (point != null)
                { // We did find a spot with just one rowingboat so we can use it
                    if (DebugMode) LogEvent($"Check availability for {type}, offering number {point.WharfNumber}");
                    return point.WharfNumber;
                }
            } // we didn't find a spot so we go on to find a completely empty spot. Or, this wasn't a rowingboat
            var freeNums = wharf1.Where(w => w.Type == BoatType.None).Select(s => s.WharfNumber).ToArray();
            if (neededPositions == 1 && freeNums.Length > 0) retval = freeNums[0];
            else
                retval = freeNums.FindConsecutives(neededPositions);
            if (retval == -1) // If we still didn't find a spot, move on to check wharf2
            {
                freeNums = wharf2.Where(w => w.Type == BoatType.None).Select(s => s.WharfNumber).ToArray();
                if (neededPositions == 1 && freeNums.Length > 0) retval = freeNums[0];
                else
                    retval = freeNums.FindConsecutives(neededPositions);

            }
            if (retval == -1)
            {
                LogEvent($"Check availability for {type}, no space found for this type!");
                deniedBoats++;
            }
            else
            {
                if (DebugMode) LogEvent($"Check availability for {type}, offering number {retval}");
            }
            return retval;
        }


        private static string GeneratePointString(int wharfNumber, int numPositions)
        {
            if (numPositions == 1) return wharfNumber.ToString();
            else return $"{wharfNumber}-{wharfNumber + (numPositions - 1)}";
        }
        static public void RegisterBoat(Boat b, bool loadedFromFile = false)
        {
            int wharfNum = b.WharfNumber;
            string takeStr = "";
            var currentWharf = wharfNum > 32 ? wharf2 : wharf1;
            b.OnLeaving = BoatLeaving;
            for (int i = 0; i < b.NumPositions; i++)
            {
                var point = currentWharf.Where(p => p.WharfNumber == (wharfNum + i)).FirstOrDefault();
                point.Type = b.Type;
                if (point.Boats.Count == 0) // We check so that this point doesn't contain a rowingboat
                    takeStr = $", taking {b.NumPositions} slots";
                
                point.Boats.Add(b);
            }
            currentBoats.Add(b);
            if (!loadedFromFile)
                LogEvent($"Registering boat {b.Type} with ID {b.Identity}, wharfnumber {b.WharfNumber}{takeStr}");
        }
        static private void BoatLeaving(Boat b)
        {
            // This method will be set to each boat's delegate so it can call it when it is time to leave
            int wharfNum = b.WharfNumber;
            string freeStr = "";
            var currentWharf = wharfNum > 32 ? wharf2 : wharf1;
            for (int i = 0; i < b.NumPositions; i++)
            {
                var point = currentWharf.Where(p => p.WharfNumber == (wharfNum + i)).FirstOrDefault();
                point.Boats.Remove(b);
                if (b.Type != BoatType.RowingBoat)
                {
                    point.Type = BoatType.None;
                    freeStr = $", freeing {b.NumPositions} slots";
                }
                else
                { // We might have another rowingboat here so check it
                    if (point.Boats.Count == 0)
                    {
                        point.Type = BoatType.None;
                        // and flag that we're actually freeing this slot, since this was the only rowingboat here
                        freeStr = $", freeing {b.NumPositions} slots";
                    }
                }

            }
            // We need to use a separate deletelist since this is run during tick and we can't modify the list then.

            deleteList.Add(b);
            LogEvent($"UnRegistering boat {b.Type} with ID {b.Identity}, wharfnumber {b.WharfNumber}{freeStr}");
        }
    }
}
