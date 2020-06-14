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
    /// Interaction logic for CheckClientInsurancesPreview.xaml
    /// </summary>
    public partial class CheckClientInsurancesPreview : Window
    {
        public CheckClientInsurancesPreview(int count , string Egn)
        {
            InitializeComponent();
            FillData(count , Egn);
        }

        public void FillData(int count, string Egn)
        {
            //count and Egn are not checked for validity, since they are checked in RefPage.xaml.cs
            Customer temp = new Customer();
            List<Insurance> listOfInsurances = new List<Insurance>();

            //Check for the owner in the Database
            if (ManageDB.CheckForOwnerInDB(Egn, ref temp))
            {
                ClientNameLbl.Content = temp.Name;

                //Get list of the insurances for the current client by EGN from Database
                ManageDB.GetClientInsurances(Egn, ref listOfInsurances);
                DataGrid.ItemsSource = listOfInsurances;

                //Display only needed columns from Database
                var col = new DataGridTextColumn();
                col.Header = "Застраховка №";
                col.Binding = new Binding("Id");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "МПС №";
                col.Binding = new Binding("VehicleID");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "Премия(лв.)";
                col.Binding = new Binding("PolicyPrice");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "Валидна от";
                col.Binding = new Binding("IssueDate");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "Валидна до";
                col.Binding = new Binding("DueDate");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "Тип застраховка";
                col.Binding = new Binding("TypeOfInsurance");
                DataGrid.Columns.Add(col);

                col = new DataGridTextColumn();
                col.Header = "Застрахователна сума в лв. (Каско)";
                col.Binding = new Binding("InsuranceMoney");
                DataGrid.Columns.Add(col);

                //Set the data grid to read only to prevent change of the data from Database
                DataGrid.IsReadOnly = true;
            }
            else
            {
                ClientNameLbl.Content = "";
                DataGrid.Visibility = Visibility.Hidden;
                PrintBtn.IsEnabled = false;
                MessageBox.Show("Няма намерени застраховки за този клиент!");
            }
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
