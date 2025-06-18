using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MyBank_Draft2.Pages.Main
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        Database _localdb;

        public SignUp()
        {
            InitializeComponent();
            LoadDatabase();
        }

        void LoadDatabase()
        {
            _localdb = new Database();
        }

        private void SignUpWithGoogle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(fnameIN.Text) || String.IsNullOrEmpty(lnameIN.Text) || String.IsNullOrEmpty(emailIN.Text) || String.IsNullOrEmpty(passIN.Text))
            {
                return;
            }

            else
            {
                AddUser();
            }
        }

        private void AddUser()
        {
            if (!_localdb.db.Customers.Any(c => c.Customer_Email == emailIN.Text.ToString()))
            {
                var lastUID = _localdb.db.Users.OrderByDescending(u => u.Users_ID).FirstOrDefault();
                var lastCID = _localdb.db.Customers.OrderByDescending(c => c.Customer_ID).FirstOrDefault();

                string newCID = "C" + (int.Parse(lastCID.Customer_ID.Substring(1))).ToString("D2");
                string newUID = "C" + (int.Parse(lastUID.Users_ID.Substring(1))).ToString("D2");
                int UWID = GenerateNum();

                var _newUser = new User
                {
                    Users_ID = newUID,
                    Role_ID = "R01"
                };

                var _newUserWallet = new UserWallet
                {
                    UserWallet_ID = UWID,
                    UserWallet_Balance = 0,
                    UserWallet_Currency = "USD"
                };

                var _newCustomer = new Customer
                {
                    Customer_ID = newCID,
                    Users_ID = newUID,
                    UserWallet_ID = UWID,
                    Customer_FirstName = fnameIN.Text,
                    Customer_LastName = lnameIN.Text,
                    Customer_Email = emailIN.Text,
                    Customer_Password = passIN.Text,
                };

                _localdb.db.Users.InsertOnSubmit(_newUser);
                SubmitChanges();
                _localdb.db.UserWallets.InsertOnSubmit(_newUserWallet);
                SubmitChanges();
                _localdb.db.Customers.InsertOnSubmit(_newCustomer);
                SubmitChanges();
            }
        }
        private int GenerateNum()
        {
            Random rnd = new Random();
            int generatedNum;

            while (true)
            {
                generatedNum = rnd.Next(0, 9999999);

                if (!_localdb.db.UserWallets.Any(u => u.UserWallet_ID == generatedNum))
                {
                    break;
                }

            }

            return generatedNum;
        }
        private void SubmitChanges()
        {
            try
            {
                _localdb.db.SubmitChanges();
                LoadDatabase();
            }
            catch
            {
                return;
            }
        }
    }
}

