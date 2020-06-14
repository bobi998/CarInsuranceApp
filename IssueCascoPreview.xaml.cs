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
using System.Windows.Shapes;

namespace CarInsurance
{
    /// <summary>
    /// Interaction logic for IssueCascoPreview.xaml
    /// </summary>
    public partial class IssueCascoPreview : Window
    {
        public IssueCascoPreview(Insurance insure , Vehicle veh , Customer cust)
        {
            InitializeComponent();

            //Check for null onjects passed to the constructor
            if (null != insure && null != veh && null != cust)
            {
                //Fill preview window
                FillPreview(insure, veh , cust);
            }
        }

        void FillPreview(Insurance arg , Vehicle arg2 , Customer arg3)
        {
            PolicyTxt.Content = arg.Id;
            OwnerTxt.Content = arg3.Name;
            CoupeTypeTxt.Content = arg2.CoupeType.ToString();
            VehTypeTxt.Content = arg2.VehType.ToString();
            BrandTxt.Content = arg2.Brand;
            ModelTxt.Content = arg2.Model;
            ColorTxt.Content = arg2.Color;
            PriceTxt.Content = arg.InsuranceMoney;
            YearsTxt.Content = DateTime.Now.Year - arg2.Year;
            VINTxt.Content = arg2.VIN;
            ValidFrom.Content = arg.IssueDate;
            ValidDue.Content = arg.DueDate;
            CascoPrice.Content = arg.PolicyPrice + "лв.";
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                this.Visibility = Visibility.Hidden;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    //Print only the needed things, not the whole window
                    printDialog.PrintVisual(Print, "Insurance");
                }
            }
            finally
            {
                this.IsEnabled = true;
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
