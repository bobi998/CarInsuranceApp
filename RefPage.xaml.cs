using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CarInsurance
{
    /// <summary>
    /// Interaction logic for RefPage.xaml
    /// </summary>
    public partial class RefPage : Page
    {
        public RefPage()
        {
            InitializeComponent();
            //Fill automatic references in the page
            FillAvgPrice_Age();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EgnLbl.Visibility = Visibility.Visible;
            EgnTxt.Visibility = Visibility.Visible;
            EgnTxt.IsEnabled = true;

            VinLbl.Visibility = Visibility.Hidden;
            VinTxt.Visibility = Visibility.Hidden;
            VinTxt.IsEnabled = false;
            VinTxt.Text = "";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            EgnLbl.Visibility = Visibility.Hidden;
            EgnTxt.Visibility = Visibility.Hidden;
            EgnTxt.IsEnabled = false;
            EgnTxt.Text = "";

            VinLbl.Visibility = Visibility.Hidden;
            VinTxt.Visibility = Visibility.Hidden;
            VinTxt.IsEnabled = false;
            VinTxt.Text = "";
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if(false != CheckAllClients.IsChecked)
            {
                //If check for all clients in the database is requsted
                CheckAllClientsPreview();
            }
            if(false != CheckClientInsurances.IsChecked)
            {
                //If check for specific client insurances is requsted
                CheckClientInsurancesPreview();
                EgnTxt.Text = "";
            }
            if(false != CheckInsurancesByVIN.IsChecked)
            {
                //If check for specific car is requested
                CheckInsurancesByVinPreview();
                VinTxt.Text = "";
            }
        }

        void CheckAllClientsPreview()
        {
            //Open preview window for the reference
            CheckAllClientsPreview preview = new CheckAllClientsPreview();
            preview.Show();
        }

        void CheckClientInsurancesPreview()
        {
            int count = -1;

            //Check data validity in Egn text control
            if (null == EgnTxt.Text)
            {
                //Egn text control is empty
                MessageBox.Show("Невалидно ЕГН!");
            }
            else if(10 != EgnTxt.Text.Length)
            {
                //Egn text control contains invalid input
                MessageBox.Show("Невалидно ЕГН!");
            }
            else
            {
                //Get the number of insurances for current client
                count = ManageDB.GetInsuranceCountByEgn(EgnTxt.Text);

                //Check the number of insurances found
                if (count == -1)
                {
                    //No insurances found - no need to open preview
                    //Display error msg
                    MessageBox.Show("Няма застраховки на този клиент!");
                }
                else
                {
                    //Open preview of client insurances
                    CheckClientInsurancesPreview preview = new CheckClientInsurancesPreview(count, EgnTxt.Text);
                    preview.Show();
                }
            }
        }

        void CheckInsurancesByVinPreview()
        {
            //Check Vin text control for invalid data
            if(VinTxt.Text.Length != 0)
            {
                List<Insurance> tempInsurances = new List<Insurance>();
                //Check if vehicle is in the database
                if (false != ManageDB.CheckForValidInsurancesByVIN(VinTxt.Text , ref tempInsurances))
                {
                    //Check if there are insurances for the current vehicle
                    if (null != tempInsurances)
                    {
                        //Open preview of the insurances
                        CheckInsurancesByVinPreview preview = new CheckInsurancesByVinPreview(VinTxt.Text, tempInsurances);
                        preview.Show();
                    }
                    else
                    {
                        //No insurances found - no need to open preview
                        //Show error msg
                        MessageBox.Show("Няма валидни застраховки за това МПС!");
                    }
                }
                else
                {
                    //Vehicle is not in database
                    //Show error msg
                    MessageBox.Show("Несъществуващ VIN!");
                }
            }
            else
            {
                //Input in Vin text control is invalid
                //Show error msg
                MessageBox.Show("Некоректен VIN!");
            }
        }

        void FillAvgPrice_Age()
        {
            //Calculate average price of the vehicles in the database
            double temp = ManageDB.CalcAvgPriceOfVehicles();
            if (0 == temp)
            {
                AvgPriceLbl.Content = "";
            }
            else
            {
                AvgPriceLbl.Content = temp.ToString("F") + "лв.";
            }

            //Calculate average age of the vehicles in the database
            temp = ManageDB.CalcAvgAgeOfVehicles();
            if(0 == temp)
            {
                AvgAgeLbl.Content = "";
            }
            else
            {
                AvgAgeLbl.Content = temp.ToString("F") + "г.";
            }
            
        }

        private void CheckInsurancesByVIN_Checked(object sender, RoutedEventArgs e)
        {
            EgnLbl.Visibility = Visibility.Hidden;
            EgnTxt.Visibility = Visibility.Hidden;
            EgnTxt.IsEnabled = false;
            EgnTxt.Text = "";

            VinLbl.Visibility = Visibility.Visible;
            VinTxt.Visibility = Visibility.Visible;
            VinTxt.IsEnabled = true;
        }
    }
}
