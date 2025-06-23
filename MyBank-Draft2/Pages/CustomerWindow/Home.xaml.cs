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
using MaterialDesignThemes.Wpf;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Globalization;


namespace MyBank_Draft2.Pages.CustomerWindow
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        Database _localdb;
        string localUser;
        string currencyUsed;
        public Home(string user)
        {
            InitializeComponent();
            localUser = user;
            LoadDatabase();
        }

        void LoadDatabase()
        {
            _localdb = new Database();
            LoadExpense();
            LoadIncome();
            RetrieveUser();
            RetrieveWallet();
        }

        void LoadIncome()
        {
            Random rnd = new Random();

            var _income = from t in _localdb.db.Transactions
                            where t.Users_ID == localUser
                            join c in _localdb.db.Categories on t.Category_ID equals c.Category_ID
                            where c.Category_Type == "Income"
                            group t by t.Transaction_Desc into g
                            select new
                            {
                                CategoryName = g.Key,
                                TotalAmount = g.Sum(t => t.Amount)
                            };

            SeriesCollection pieSeries = new SeriesCollection();
            int colorIndex = 0;

            foreach (var item in _income.ToList())
            {
                if (item.TotalAmount > 0)
                {
                    byte r = (byte)rnd.Next(256); // 0-255
                    byte g = (byte)rnd.Next(256); // 0-255
                    byte b = (byte)rnd.Next(256); // 0-255

                    SolidColorBrush randomColor = new SolidColorBrush(Color.FromRgb(r, g, b));

                    pieSeries.Add(new PieSeries
                    {
                        Title = item.CategoryName,
                        Values = new ChartValues<double> { (double)item.TotalAmount },
                        DataLabels = true,
                        LabelPoint = chartPoint => $"{chartPoint.SeriesView.Title}: {chartPoint.Y:N2} ({chartPoint.Participation:P1})",
                        Fill = randomColor
                    });

                    colorIndex++;
                }
            }

            doughnutIncome.Series = pieSeries;  
        }
        void LoadExpense()
        {
            Random rnd = new Random();

            var _expenses = from t in _localdb.db.Transactions
                                    where t.Users_ID == localUser
                                    join c in _localdb.db.Categories on t.Category_ID equals c.Category_ID
                                    where c.Category_Type == "Expense"
                                    group t by t.Transaction_Desc into g
                                    select new
                                    {
                                        CategoryName = g.Key,
                                        TotalAmount = g.Sum(t => t.Amount)
                                    };

             SeriesCollection pieSeries = new SeriesCollection();

                foreach (var item in  _expenses.ToList())
                {
                    if (item.TotalAmount > 0)
                    {
                        byte r = (byte)rnd.Next(256); // 0-255
                        byte g = (byte)rnd.Next(256); // 0-255
                        byte b = (byte)rnd.Next(256); // 0-255

                        SolidColorBrush randomColor = new SolidColorBrush(Color.FromRgb(r, g, b));

                        pieSeries.Add(new PieSeries
                        {
                            Title = item.CategoryName,
                            Values = new ChartValues<double> { (double)item.TotalAmount },
                            DataLabels = true,
                            LabelPoint = chartPoint => $"{chartPoint.SeriesView.Title}: {chartPoint.Y:N2} ({chartPoint.Participation:P1})",
                            Fill = randomColor
                        });

                    }
                }

                doughnutTransactions.Series = pieSeries;
        }
        void RetrieveUser()
        {
            var username = (from c in _localdb.db.Customers
                            where c.Users_ID == localUser
                            select c.Customer_FirstName).FirstOrDefault();

            usernameTB.Text = username.ToString() + "!";
        }
        void RetrieveWallet()
        {
            var userWallet = (from c in _localdb.db.Customers
                             where c.Users_ID == localUser
                             join u in _localdb.db.UserWallets on c.UserWallet_ID equals u.UserWallet_ID
                             select u).FirstOrDefault();

            RetrieveCurrency(userWallet);

            balanceTB.Text = currencyUsed + userWallet.UserWallet_Balance.ToString();

            RetrieveCurrencyIcon(userWallet.UserWallet_Currency.ToString());
            RetrieveExpenses();
            RetrieveIncome();
        }
        void RetrieveExpenses()
        {
            var _expenses = from t in _localdb.db.Transactions
                            where t.Users_ID == localUser
                            join c in _localdb.db.Categories on t.Category_ID equals c.Category_ID
                            where c.Category_Type == "Expense"
                            group t by t.Transaction_Desc into g
                            select new
                            {
                                CategoryName = g.Key,
                                TotalAmount = g.Sum(t => t.Amount)
                            };

            expenseTB.Text = currencyUsed + _expenses.Sum(e => e.TotalAmount).ToString();
        }
        void RetrieveIncome()
        {
            var _income = from t in _localdb.db.Transactions
                          where t.Users_ID == localUser
                          join c in _localdb.db.Categories on t.Category_ID equals c.Category_ID
                          where c.Category_Type == "Income"
                          group t by t.Transaction_Desc into g
                          select new
                          {
                              CategoryName = g.Key,
                              TotalAmount = g.Sum(t => t.Amount)
                          };

            incomeTB.Text = currencyUsed + _income.Sum(e => e.TotalAmount).ToString();
        }
        void RetrieveCurrency(UserWallet wallet)
        {
            string walletCurrency = wallet.UserWallet_Currency.ToString();
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (region.ISOCurrencySymbol == walletCurrency)
                {
                    currencyUsed = region.CurrencySymbol;
                }
            }
        }
        void RetrieveCurrencyIcon(string currency)
        {
            string newCurrency = null;

            for (int x = 0; x < currency.Length; x++)
            {
                if (x == 0)
                {
                    newCurrency += currency[x];
                }
                else
                {
                    newCurrency += char.ToLower(currency[x]);
                }
            }

            CurrencyIcon.Kind = (PackIconKind)Enum.Parse(typeof(PackIconKind), "Currency" + newCurrency);
        }
    }
}
