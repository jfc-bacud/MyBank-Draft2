using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MyBank_Draft2
{
    internal class Database
    {
        public MyBankDataClassDataContext db;

        // CONNECTIONSTRING FOR LOCAL, CONNECTIONSTRING1 FOR THINGS IN SCHOOL DESKTOPS

        public Database()
        {
            db = new MyBankDataClassDataContext(Properties.Settings.Default.MyBankConnectionString1);

        }

        public List<Customer> GetCustomer()
        {
            List<Customer> customerList = (from c in db.Customers
                                           select c).ToList();
                
            return customerList;
        }
        public List<Admin> GetAdmin()
        {
            List<Admin> adminList = (from a in db.Admins
                                     select a).ToList();

            return adminList;
        }
        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactionList = (from t in db.Transactions
                                           select t).ToList();

            return transactionList;
        }
        public List<UserWallet> GetUserWaller()
        {
            List<UserWallet> walletList = (from u in db.UserWallets
                                                 select u).ToList();

            return walletList;
        }
    }
}

