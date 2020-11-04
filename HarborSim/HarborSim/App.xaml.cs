using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using HarborLib;
namespace HarborSim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static private List<WharfItem> wf1 = new List<WharfItem>();
        static private List<WharfItem> wf2 = new List<WharfItem>();
        public ObservableCollection<WharfItem> Src1 = new ObservableCollection<WharfItem>();
        public ObservableCollection<WharfItem> Src2 = new ObservableCollection<WharfItem>();
        private bool running;
        public Action<HarborStats> UpdateUI { get; set; }
        private DispatcherTimer dayTimer = new DispatcherTimer();
        
        void App_Startup(object sender, StartupEventArgs e)
        {
//            Task.Run(() => runLoop());
        }

        internal void InitialLoad()
        {
            SoundFX.Init();
            if (!Harbor.LoadBoats()) HarborUtils.CreateRandomBoats(Harbor.NumRandomBoats); // Make sure we have something to show, either from file or random new ones

            SoundFX.PlayStartSound();
            SoundFX.StartAmbience();
            UpdateHarborContent(); // we do this so we load the initial content
            dayTimer.Tick += DayTick;
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            SoundFX.Close();
            Harbor.SaveBoats();
        }
        public void StartAutomaticTime(bool auto)
        {
            if (auto)
            {
                dayTimer.Interval = Harbor.TimeBetweenDays;

                dayTimer.Start();
            }
            else dayTimer.Stop();
        }
        private void DayTick(object sender, EventArgs e)
        {
            MoveToNextDay();
            UpdateHarborContent();
            
        }

        public void UpdateHarborContent()
        {
            if (UpdateUI != null) Dispatcher.BeginInvoke(new Action(() =>
            {
                Harbor.GetWharfItems(1, Src1);
                Harbor.GetWharfItems(2, Src2);
                UpdateUI(Harbor.GetStatistics());
            }));
        }

        public void MoveToNextDay()
        {
            SoundFX.PlayHorn();
            Harbor.Tick();
            HarborUtils.CreateRandomBoats(Harbor.NumRandomBoats);
        }
    }
}
