using HarborLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation;
namespace HarborSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App curApp = (App)Application.Current;
        public MainWindow()
        {
            InitializeComponent();
            
            curApp.UpdateUI = onUpdateUI;
            lvWarf1.ItemsSource = curApp.Src1;
            lvWarf2.ItemsSource = curApp.Src2;
            Harbor.EventLoggerAction = AddToLog;
            btnTimeBetweenDays.Content = $"{Harbor.TimeBetweenDays.TotalSeconds} S melan dagar";
            curApp.InitialLoad();
        }

        private void onUpdateUI(HarborStats stats)
        {
            txtRBoats.Text = stats.NumRowingBoats.ToString();
            txtPBoats.Text = stats.NumPowerBoats.ToString();
            txtSBoats.Text = stats.NumSailBoats.ToString();
            txtCatamaran.Text = stats.NumCatamarans.ToString();
            txtCBoats.Text = stats.NumCargoBoats.ToString();
            txtFree.Text = stats.FreeSlots.ToString();
            txtDenied.Text = stats.DeniedBoats.ToString();
            txtTotalWeight.Text = stats.TotalWeight.ToString();
            txtAverageSpeed.Text = stats.AverageMaxSpeedKMPH.ToString();
            txtDay.Text = Harbor.Day.ToString();
        }

        private void cbAutomaticTime_Click(object sender, RoutedEventArgs e)
        {

            if (cbAutomaticTime.IsChecked.Value)
            {
                Harbor.AutomaticTimeSwitching = true;
                
                btnNextDay.Visibility = Visibility.Collapsed;
                btnTimeBetweenDays.Visibility = Visibility.Visible;
            }
            else
            {
                Harbor.AutomaticTimeSwitching = false;
                btnNextDay.Visibility = Visibility.Visible;
                btnTimeBetweenDays.Visibility = Visibility.Collapsed;
            }
            curApp.StartAutomaticTime(cbAutomaticTime.IsChecked.Value);
        }

        private void btnNextDay_Click(object sender, RoutedEventArgs e)
        {
            curApp.MoveToNextDay();
            curApp.UpdateHarborContent();

        }

        private void cbShowLog_Click(object sender, RoutedEventArgs e)
        {
            if (cbShowLog.IsChecked.Value) txtLog.Visibility = Visibility.Visible;
            else txtLog.Visibility = Visibility.Collapsed;
        }
        private void AddToLog(string text)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                txtLog.Text += $"{text}\n";
                txtLog.ScrollToEnd();
            }));
        }

        private void sldBoatsPerDay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Harbor.NumRandomBoats = (int)sldBoatsPerDay.Value;
            
        }

        private void btnTimeBetweenDays_Click(object sender, RoutedEventArgs e)
        {
            btnTimeBetweenDays.Visibility = Visibility.Collapsed;
            pnlDayAdjust.Visibility = Visibility.Visible;
            sldSecsDays.Value = Harbor.TimeBetweenDays.TotalSeconds;
            curApp.StartAutomaticTime(false);
        }


        private void btnApplyTimeBetweenDays_Click(object sender, RoutedEventArgs e)
        {
            pnlDayAdjust.Visibility = Visibility.Collapsed;
            Harbor.TimeBetweenDays = TimeSpan.FromSeconds((int)sldSecsDays.Value);
            btnTimeBetweenDays.Visibility = Visibility.Visible;
            btnTimeBetweenDays.Content = $"{Harbor.TimeBetweenDays.TotalSeconds} S melan dagar";
            curApp.StartAutomaticTime(true);
        }
    }
}
