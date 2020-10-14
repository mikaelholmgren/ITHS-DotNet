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

namespace Calculator4_accessible
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void calcAdd_click(object sender, RoutedEventArgs e)
        {
            HandleOperations(CalcOperations.Add);

        }
        private void calcSub_click(object sender, RoutedEventArgs e)
        {
            HandleOperations(CalcOperations.Subtract);

        }
        private void calcMul_click(object sender, RoutedEventArgs e)
        {
            HandleOperations(CalcOperations.Multiply);

        }
        private void calcDiv_click(object sender, RoutedEventArgs e)
        {
            HandleOperations(CalcOperations.Divide);

        }

        private void HandleOperations(CalcOperations co)
        {
            try
            {
                int val1 = int.Parse(txtFirstNum.Text);
                int val2 = int.Parse(txtSecondNum.Text);

                switch (co)
                {
                    case CalcOperations.Add:
                        lstResult.Items.Add($"{val1} + {val2} = {val1 + val2}");
                        break;
                    case CalcOperations.Subtract:
                        lstResult.Items.Add($"{val1} - {val2} = {val1 - val2}");
                        break;
                    case CalcOperations.Multiply:
                        lstResult.Items.Add($"{val1} x {val2} = {val1 * val2}");
                        break;
                    case CalcOperations.Divide:
                        lstResult.Items.Add($"{val1} / {val2} = {val1 / val2}");
                        break;

                }
                txtFirstNum.Text = "";
                txtSecondNum.Text = "";
                // The following 2 lines are to try and focus the closest line in the result for screenreader users after calculation 
                lstResult.SelectedItem = lstResult.Items[lstResult.Items.Count - 1];
                lstResult.Focus();
            }
            catch (DivideByZeroException)
            {
                lblErrMsg.Content = "Felaktigt värde, du försöker dividera med 0!";
                lblErrMsg.Visibility = Visibility.Visible;
                // We set focus on this for screenreader users, it goes away when it looses focus
                lblErrMsg.Focus();
            }

            catch (FormatException)
            {
                lblErrMsg.Content = "Felaktigt värde, ange enbart siffror!";
                lblErrMsg.Visibility = Visibility.Visible;
                // We set focus on this for screenreader users, it goes away when it looses focus
                lblErrMsg.Focus();
            }
        }


        private void hideErrMsg(object sender, RoutedEventArgs e)
        {
            lblErrMsg.Visibility = Visibility.Hidden;
        }
    }
}
