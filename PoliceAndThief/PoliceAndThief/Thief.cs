using System.Collections.Generic;

namespace PoliceAndThief
{
    public class Thief : Person
    {
        public List<Item> StolenGoods { get; set; }
        public Thief()
        {
            StolenGoods = new List<Item>();
        }
        public override char Display()
        {
            return 'T';
        }
        public override bool CheckAction(GameboardCell c)
        {
            Citizen ci = (Citizen)c.People.Find(i => i is Citizen);
            if (ci != null)
            {
                DoAction(ci);
                return true;
            }
            else return false;
        }
        public override void DoAction(Person destination)
        {
            Citizen p = (Citizen)destination;
            // robery action, first check if victim has something to rob
            if (p.Belongings.Count > 0)
            {
                Item it = p.Belongings[Utils.GetRandomNumber(0, p.Belongings.Count)];
                StolenGoods.Add(it);
                p.Belongings.Remove(it);
                Utils.PrintEvent($"Medborgare rånad på sin {it.Name}");
                Stats.AddRob();
            }
            else Utils.PrintEvent("Tjuv försöker råna medborgare men denne har inga saker kvar!");
        }
    }
}
