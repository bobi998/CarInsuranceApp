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
    /// Interaction logic for CheckInsurancesByVinPreview.xaml
    /// </summary>
    public partial class CheckInsurancesByVinPreview : Window
    {
        public CheckInsurancesByVinPreview(string vin, List<Insurance> insurances)
        {
            InitializeComponent();
            FillData(vin, insurances);
        }

        void FillData(string vin , List<Insurance> insurances)
        {
            //vin and insurances are not checked for validity, since they are checked in RefPage.xaml.cs
            VinLbl.Content = vin;
            DataGrid.ItemsSource = insurances;

            //Display only needed columns from Databases
            var col = new DataGridTextColumn();
            col.Header = "Застраховка №";
            col.Binding = new Binding("Id");
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

            DataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
