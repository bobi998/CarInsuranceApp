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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void IssueBtn_Click(object sender, RoutedEventArgs e)
        {
            //Issue Casco button is clicked. Set the content of MainFrame to IssuePage
            MainFrame.Content = new IssuePage();
        }

        private void CalcBtn_Click(object sender, RoutedEventArgs e)
        {
            //Calculation of Casco button is clicked. Set the content of MainFrame to CalcPage
            MainFrame.Content = new CalcPage();
        }

        private void RefBtn_Click(object sender, RoutedEventArgs e)
        {
            //References button is clicked. Set the content of MainFrame to RefPage
            MainFrame.Content = new RefPage();
        }

        private void CalcLiabBtn_Click(object sender, RoutedEventArgs e)
        {
            //Calculation of Liability insurance button is clicked. Set the content of MainFrame to CalcLiabPage
            MainFrame.Content = new CalcLiabPage();
        }

        private void LiabBtn_Click(object sender, RoutedEventArgs e)
        {
            //Issue of Liability insurance button is clicked. Set the content of MainFrame to LiabPage
            MainFrame.Content = new LiabPage();
        }
    }
}
