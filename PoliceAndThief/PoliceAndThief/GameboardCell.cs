using System.Collections.Generic;

namespace PoliceAndThief
{
    public class GameboardCell
    {
        public List<Person> People { get; set; }
        public GameboardCell()
        {
            People = new List<Person>();
        }
    }
}