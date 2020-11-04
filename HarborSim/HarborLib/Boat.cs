using System;

namespace HarborLib
{
    public abstract class Boat
    {
        public BoatType Type { get; }
        public string Identity { get; }
        public string BoatTypeName { get; }
        public int Weight { get; }
        public int MaxSpeed { get; }
        public int NumPositions { get; }
        public int StayDays { get; }

        public int CurrentDay { get; internal set; }

        public int WharfNumber { get; internal set; }
        public Action<Boat> OnLeaving { get; internal set; }

        public Boat(BoatType type,
            string identity, string boatTypeName,
            int weight, int maxSpeed, int numPos, int stayDays, int day =1)
        {
            Type = type;
            Identity = identity;
            BoatTypeName = boatTypeName;
            Weight = weight;
            MaxSpeed = maxSpeed;
            NumPositions = numPos;
            StayDays = stayDays;
            CurrentDay = day;
        }
        
        public void Tick()
        {
            CurrentDay++;
            if (CurrentDay > StayDays) Leave();
        }

        private void Leave()
        {
            OnLeaving(this);
        }
        public void Arrive()
        {
            int num = Harbor.GetFreeWharfNumber(Type);
            if (num ==-1)
            {
                // This means the harbor couldn't find room for us, so nothing more to do than just leave
                return;
            }
            // great, we go on registering with the number we got
            WharfNumber = num;
            Harbor.RegisterBoat(this);
        }
        public abstract string GetUniqueProperty();

        public abstract string GetAsCSV();
    }
}