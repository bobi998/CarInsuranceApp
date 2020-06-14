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
    /// Interaction logic for IssuePage.xaml
    /// </summary>
    public partial class IssuePage : Page
    {
        public IssuePage()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(VehTypeCombo.SelectedIndex == 1)
            {
                //Enable CoupeType label and combo box only when selected item in VehTypeCombo is "Лек автомобил"
                CoupeTypeLbl.IsEnabled = true;
                CoupeTypeCombo.IsEnabled = true;
            }
            else
            {
                //Disable CoupeType label and combo box when selected item in VehTypeCombo is not "Лек автомобил"
                CoupeTypeLbl.IsEnabled = false;
                CoupeTypeCombo.SelectedIndex = -1;
                CoupeTypeCombo.IsEnabled = false;
            }
        }

        private void CalculationBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check input first
            if (false != CheckIssueData())
            {
                Customer tempCust = new Customer();
                Vehicle tempVeh = new Vehicle();

                //Check for existing owner in DB
                if (false == ManageDB.CheckForOwnerInDB(EgnTxt.Text , ref tempCust))
                {
                    tempCust = new Customer(OwnerNameTxt.Text, EgnTxt.Text);
                    ManageDB.AddCustInDB(tempCust);
                }
                //Check for existing car in DB
                if (false == ManageDB.CheckForVehInDB(VinTxt.Text, ref tempVeh))
                {
                    tempVeh = new Vehicle(BrandTxt.Text, ModelTxt.Text, DateTime.Now.Year - int.Parse(AgeTxt.Text), 0, ColorTxt.Text,
                    (VehicleType)VehTypeCombo.SelectedIndex, (CoupeType)CoupeTypeCombo.SelectedIndex, int.Parse(PowerTxt.Text), " ", VinTxt.Text, tempCust.CustomerId);
                    ManageDB.AddVehInDB(tempVeh);
                }

                //Calculate Liability Casco price
                double price = Calculations.CalcCascoPrice(int.Parse(ExpTxt.Text), (VehicleType)VehTypeCombo.SelectedIndex , int.Parse(PowerTxt.Text) , DateTime.Now.Year - int.Parse(AgeTxt.Text), double.Parse(PriceTxt.Text) , (bool)YesBtn.IsChecked);
                DateTime issueDate = DateTime.Now;
                DateTime dueDate = DateTime.Now.AddYears(1);
                //Get the id of the new policy
                int insuranceNumb = ManageDB.GetInsuranceCount() + 1;
                Insurance tempIns = new Insurance(insuranceNumb, tempCust.CustomerId, tempVeh.VehicleId , price, issueDate, dueDate, (InsuranceType)0 , double.Parse(PriceTxt.Text));
                //Save insurance in DB
                ManageDB.AddInsuranceInDB(tempIns);
                //Preview insurance
                IssueCascoPreview previewWin = new IssueCascoPreview(tempIns , tempVeh , tempCust);
                previewWin.Show();
                ClearAllControls();
            }
            else
            {
                //Invalid input -> show error msg
                MessageBox.Show("Въведени са некоректни данни!");
            }
        }

        private bool CheckIssueData()
        {
            bool bRetVal = true;

            if ((null == VehTypeCombo.SelectedItem) || (CoupeTypeCombo.IsEnabled && null == CoupeTypeCombo.SelectedItem) || 
                (string.IsNullOrWhiteSpace(BrandTxt.Text)) || (string.IsNullOrWhiteSpace(ModelTxt.Text)) || (string.IsNullOrWhiteSpace(ColorTxt.Text)) ||
                (string.IsNullOrWhiteSpace(PriceTxt.Text)) || (string.IsNullOrWhiteSpace(VinTxt.Text)) || (string.IsNullOrWhiteSpace(AgeTxt.Text)) || 
                (string.IsNullOrWhiteSpace(OwnerNameTxt.Text)) || (string.IsNullOrWhiteSpace(EgnTxt.Text)) || (string.IsNullOrWhiteSpace(PowerTxt.Text)) ||
                (string.IsNullOrWhiteSpace(ExpTxt.Text))
               )
            {
                bRetVal = false;
            }
            return bRetVal;
        }

        private void ClearAllControls()
        {
            VehTypeCombo.SelectedIndex = -1;
            CoupeTypeCombo.SelectedIndex = -1;
            BrandTxt.Text = "";
            ModelTxt.Text = "";
            ColorTxt.Text = "";
            PriceTxt.Text = "";
            VinTxt.Text = "";
            AgeTxt.Text = "";
            OwnerNameTxt.Text = "";
            EgnTxt.Text = "";
            PowerTxt.Text = "";
            ExpTxt.Text = "";
        }
    }
}
