using MyBank_Draft2.Pages.Main;
using MyBank_Draft2.Windows.Main;
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
        SignUp newUser;
        Login login;

        public MainWindow()
        {
            InitializeComponent();
            LoadLogin();
        }

        public void LoadLogin()
        {
            login = new Login();
            StartupFrame.Content = login;
        }
       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void NavToNew()
        {
            newUser = new SignUp();
            StartupFrame.Content = newUser;
        }
    }
}
