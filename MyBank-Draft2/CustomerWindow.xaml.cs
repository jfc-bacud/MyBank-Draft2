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
using LiveCharts;
using MaterialDesignColors;
using MaterialDesignThemes;
using MyBank_Draft2.Pages.CustomerWindow;

namespace MyBank_Draft2
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        Home homePage;
        Database _localdb;
        TransactionsPage transactionPage;
        string localUser;

        public CustomerWindow(string userEmail)
        {
            InitializeComponent();
            RetrieveUser(userEmail);
            ViewHome();
        }

        public void ViewHome()
        {
            homeBTN.Background = new SolidColorBrush(Colors.Green);
            homePage = new Home(localUser);
            windowFrame.Content = homePage;
        }

        public void RetrieveUser(string userEmail)
        {
            _localdb = new Database();

            var user = (from c in _localdb.db.Customers
                       where c.Customer_Email == userEmail
                       select c.Users_ID).FirstOrDefault();

            localUser = user.ToString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewHome();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            transactionPage = new TransactionsPage(localUser);
            windowFrame.Content = transactionPage;
        }
    }
}
