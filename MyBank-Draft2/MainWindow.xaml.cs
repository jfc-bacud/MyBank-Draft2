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

namespace MyBank_Draft2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        string role;
        public MainWindow()
        {
            InitializeComponent();
            LoadDatabase();
        }

        public void LoadDatabase()
        {
            db = new Database();
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(userIN.Text) || String.IsNullOrEmpty(passIN.Password))
            {
                MessageBox.Show("Error 1");
                return;
            }

            if (UserExists(out role))
            {
                if (VerifiedPassword(role))
                {
                    OpenWindow(role);
                }
            }
        }

        private bool UserExists(out string role)
        {
            var possibleCustomer = (from c in db.GetCustomer()
                                    where c.Customer_Email == userIN.Text
                                    select c).FirstOrDefault();

            var possibleAdmin = (from a in db.GetAdmin()
                                    where a.Admin_Email == userIN.Text
                                    select a).FirstOrDefault();

            if (possibleCustomer != null)
            {
                role = "Customer";
                return true;
            }
            else if (possibleCustomer != null)
            {
                role = "Admin";
                return true;
            }
            else
            {
                role = null;
                return false;
            }
        }
        private bool VerifiedPassword(string role)
        {
            if (role == "Customer")
            {
                var user = (from c in db.GetCustomer()
                            where c.Customer_Email == userIN.Text
                            select c).FirstOrDefault();

                if (user.Customer_Password == passIN.Password)
                {
                    return true;
                }
            }
            else if (role == "Admin")
            {
                var user = (from a in db.GetAdmin()
                            where a.Admin_Email == userIN.Text
                            select a).FirstOrDefault();

                if (user.Admin_Password == passIN.Password)
                {
                    return true;
                }
            }

            return false;
        }
        private void OpenWindow(string role)
        {
            if (role == "Customer")
            {
                CustomerWindow customerWindow = new CustomerWindow();
                customerWindow.Show();
                this.Close();
            }
            else if (role == "Admin")
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
        }
    }
}
