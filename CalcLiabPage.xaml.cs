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

namespace CarInsurance
{
    /// <summary>
    /// Interaction logic for CalcLiabPage.xaml
    /// </summary>
    public partial class CalcLiabPage : Page
    {
        public CalcLiabPage()
        {
            InitializeComponent();
        }

        private void CalcLiabBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check input first
            if (CheckDataInput())
            {
                double cost = Calculations.CalcLiabPrice((VehicleType)VehTypeCmb.SelectedIndex , int.Parse(ЕxpTxt.Text), int.Parse(PowerTxt.Text), int.Parse(EngCapTxt.Text), FuelTypeCmb.SelectedIndex, DateTime.Now.Year - int.Parse(FirstRegTxt.Text));
                CostLbl.Content = cost + "лв.";
            }
            else
            {
                //Invalid input -> show error msg
                MessageBox.Show("Некоректни данни!");
            }
        }

        private bool CheckDataInput()
        {
            bool bRetVal = false;

            if ( (null != VehTypeCmb.SelectedItem) &&
                 (null != FuelTypeCmb.SelectedItem) &&
                 ((string.IsNullOrWhiteSpace(ЕxpTxt.Text)) || (0 <= int.Parse(ЕxpTxt.Text))) &&
                 ((!string.IsNullOrWhiteSpace(PowerTxt.Text)) && (0 < int.Parse(PowerTxt.Text))) &&
                 ((!string.IsNullOrWhiteSpace(EngCapTxt.Text)) && (0 < int.Parse(EngCapTxt.Text))) &&
                 ((!string.IsNullOrWhiteSpace(FirstRegTxt.Text)) && (0 < int.Parse(FirstRegTxt.Text)))
               )
            {
                bRetVal = true;
            }

            return bRetVal;
        }
    }
}
