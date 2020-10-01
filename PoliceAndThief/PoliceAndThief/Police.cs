using System.Collections.Generic;

namespace PoliceAndThief
{
    public class Police : Person
    {
        public List<Item> Confiscated { get; set; }
        public Police()
        {
            Confiscated = new List<Item>();
        }
        public override char Display()
        {
            return 'P';
        }
        public override bool CheckAction(GameboardCell c)
        {
            Thief ti = (Thief)c.People.Find(i => i is Thief);
            if (ti != null)
            {
                DoAction(ti);
                return true;
            }
            else return false;
        }
        public override void DoAction(Person destination)
        {
            Thief p = (Thief)destination;
            // Police confiscate action, first check if thief has stolen something
            if (p.StolenGoods.Count > 0)
            {
                int stolenCount = p.StolenGoods.Count;
                foreach (Item it in p.StolenGoods)
                {
                    Confiscated.Add(it);
                }
                p.StolenGoods.Clear();
                Utils.PrintEvent($"Polis sätter tjuv i fängelse och beslagtar allt stöldgods ({stolenCount} saker)");
                Stats.AddCatch();
                Prison.PutInPrison(p);
            }
            else Utils.PrintEvent("Polisen möter en misstänkt tjuv, men han har inget stöldgods på sig..");
        }

    }
}
