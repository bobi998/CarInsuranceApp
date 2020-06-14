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
    /// Interaction logic for CheckAllClientsPreview.xaml
    /// </summary>
    public partial class CheckAllClientsPreview : Window
    {
        public CheckAllClientsPreview()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CarInsurance.CarInsurancesDatabaseDataSet carInsurancesDatabaseDataSet = ((CarInsurance.CarInsurancesDatabaseDataSet)(this.FindResource("carInsurancesDatabaseDataSet")));
            // Load data into the table Customers. You can modify this code as needed.
            CarInsurance.CarInsurancesDatabaseDataSetTableAdapters.CustomersTableAdapter carInsurancesDatabaseDataSetCustomersTableAdapter = new CarInsurance.CarInsurancesDatabaseDataSetTableAdapters.CustomersTableAdapter();
            carInsurancesDatabaseDataSetCustomersTableAdapter.Fill(carInsurancesDatabaseDataSet.Customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
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
