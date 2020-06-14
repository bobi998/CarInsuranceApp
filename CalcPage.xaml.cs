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
    /// Interaction logic for CalcPage.xaml
    /// </summary>
    public partial class CalcPage : Page
    {
        public CalcPage()
        {
            InitializeComponent();
        }

        private void CalculationBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check input first
            if (CheckDataInput())
            {
                //Valid input -> calculate casco price
                double cost = Calculations.CalcCascoPrice(int.Parse(OwnerЕxpTxt.Text), (VehicleType)VehTypeCmb.SelectedIndex, int.Parse(PowerTxt.Text), int.Parse(AgeTxt.Text), double.Parse(PriceTxt.Text) , (bool)YesBtn.IsChecked);
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

            if( (null != VehTypeCmb.SelectedItem) &&
                ( (string.IsNullOrWhiteSpace(OwnerЕxpTxt.Text)) || (0 <= int.Parse(OwnerЕxpTxt.Text)) ) && 
                ( (!string.IsNullOrWhiteSpace(PowerTxt.Text)) && (0 < int.Parse(PowerTxt.Text)) ) &&
                ( (!string.IsNullOrWhiteSpace(AgeTxt.Text)) && (0 < int.Parse(AgeTxt.Text)) ) && 
                ( (!string.IsNullOrWhiteSpace(PriceTxt.Text)) && (0 < double.Parse(PriceTxt.Text)) )
              )
            {
                bRetVal = true;
            }

            return bRetVal;
        }
    }
}
