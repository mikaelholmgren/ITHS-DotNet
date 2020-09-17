namespace Forrest
{
    internal class Animal
    {
        public string Name { get; set; }
        public bool Nocturnal { get; set; }
        public bool HaveWings { get; set; }
        public string CustomAction { get; set; }
        public string GetActionMessage(bool day)
        {
            string actionString = CustomAction != null ? CustomAction : $"{Name}en springer omkring och letar mat";
            bool shouldBeAwake = false;
            if (!Nocturnal && day) shouldBeAwake = true;
            else if (Nocturnal && !day) shouldBeAwake = true;
            // i annat fall så är det sover
            if (HaveWings) actionString = $"{Name}en flyger omkring och letar mat";
            else if (Nocturnal) actionString = $"{Name} smyger omkring och jagar sitt byte";
            if (shouldBeAwake) return actionString;
            else return $"{Name}en sover";
        }

    }
}