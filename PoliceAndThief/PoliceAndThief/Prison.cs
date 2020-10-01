using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace PoliceAndThief
{
   public class Prison
    {
        static List<PrisonItem> prisoners;
        static Prison()
        {
            prisoners = new List<PrisonItem>();
        }
        public static void PutInPrison(Person p)
        {
            prisoners.Add(new PrisonItem(p));
        }
        public static bool CheckIfInPrison(Person p)
        {
            PrisonItem pi = prisoners.Find(x => x.Prisoner == p);
            if (pi != null) return true;
            else return false;
        }
        public static void TickAndRelease()
        {
            var now = DateTime.Now;
            List<PrisonItem> releaseList = new List<PrisonItem>();
            foreach (PrisonItem i in prisoners)
            {
                TimeSpan prisonTime = now - i.StartTime;
                if (prisonTime.TotalSeconds >30)
                {
                    releaseList.Add(i);
                    Utils.PrintEvent($"Fånge släppt ur fängelse efter {prisonTime.TotalSeconds} sekunder");
                }
            }
            foreach (var item in releaseList)
            {
                prisoners.Remove(item);
            }

        }
        public static string GetPrisonStatus()
        {
            return $" och {prisoners.Count} fångar i fängelse";
        }
        public static List<PrisonItem> GetCurrentPrisoners()
        {
            return prisoners;
        }
    }
}
