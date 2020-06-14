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
    /// Interaction logic for LiabPage.xaml
    /// </summary>
    public partial class LiabPage : Page
    {
        public LiabPage()
        {
            InitializeComponent();
        }

        private void IssueLiabBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check input first
            if (false != CheckLiabData())
            {
                Customer tempCust = new Customer();
                Vehicle tempVeh = new Vehicle();
                //Check for existing owner in DB
                if (false == ManageDB.CheckForOwnerInDB(EgnTxt.Text, ref tempCust))
                {
                    tempCust = new Customer(OwnerNameTxt.Text, EgnTxt.Text);
                    ManageDB.AddCustInDB(tempCust);
                }
                //Check for existing car in DB
                if (false == ManageDB.CheckForVehInDB(VinTxt.Text, ref tempVeh))
                {
                    tempVeh = new Vehicle(MakeTxt.Text, ModelTxt.Text, int.Parse(YearTxt.Text), int.Parse(MonthTxt.Text), " ",
                    (VehicleType)VehTypeCmb.SelectedIndex, (CoupeType)7, int.Parse(PowerTxt.Text), " ", VinTxt.Text, tempCust.CustomerId);
                    ManageDB.AddVehInDB(tempVeh);
                }
                
                //Calculate Liability Insurance price
                double price = Calculations.CalcLiabPrice((VehicleType)VehTypeCmb.SelectedIndex,int.Parse(ExpTxt.Text), int.Parse(PowerTxt.Text),
                                                           int.Parse(EngCapTxt.Text), FuelTypeCmb.SelectedIndex , int.Parse(YearTxt.Text));
                DateTime issueDate = DateTime.Now;
                DateTime dueDate = DateTime.Now.AddYears(1);
                //Get the id of the new policy
                int insuranceNumb = ManageDB.GetInsuranceCount() + 1;

                Insurance tempIns = new Insurance(insuranceNumb, tempCust.CustomerId , tempVeh.VehicleId , price , issueDate , dueDate , (InsuranceType)1 , 0);
                //Save insurance in DB
                ManageDB.AddInsuranceInDB(tempIns);
                //Preview insurance
                IssueLiabPreview previewWin = new IssueLiabPreview(tempIns , tempVeh , tempCust);
                previewWin.Show();
                ClearAllControls();
            }
            else
            {
                //Invalid input -> show error msg
                MessageBox.Show("Въведени са некоректни данни!");
            }
        }

        private bool CheckLiabData()
        {
            bool bRetVal = true;

            if( (null == VehTypeCmb.SelectedItem) || (string.IsNullOrWhiteSpace(MakeTxt.Text)) || (string.IsNullOrWhiteSpace(ModelTxt.Text)) || 
                (string.IsNullOrWhiteSpace(YearTxt.Text)) || (string.IsNullOrWhiteSpace(MonthTxt.Text)) || (string.IsNullOrWhiteSpace(VinTxt.Text)) ||
                (string.IsNullOrWhiteSpace(EngCapTxt.Text)) || (null == FuelTypeCmb.SelectedItem) || (string.IsNullOrWhiteSpace(PowerTxt.Text)) || 
                (string.IsNullOrWhiteSpace(OwnerNameTxt.Text)) || (string.IsNullOrWhiteSpace(EgnTxt.Text))
              )
            {
                bRetVal = false;
            }
            return bRetVal;
        }

        private void ClearAllControls()
        {
            VehTypeCmb.SelectedIndex = -1;
            MakeTxt.Text = "";
            ModelTxt.Text = "";
            YearTxt.Text = "";
            MonthTxt.Text = "";
            VinTxt.Text = "";
            EngCapTxt.Text = "";
            FuelTypeCmb.SelectedIndex = -1;
            PowerTxt.Text = "";
            OwnerNameTxt.Text = "";
            EgnTxt.Text = "";
        }
    }
}
