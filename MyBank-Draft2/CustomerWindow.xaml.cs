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

        public CustomerWindow()
        {
            InitializeComponent();
            ViewHome();
        }

        public void ViewHome()
        {
            homePage = new Home();
            windowFrame.Content = homePage;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewHome();
        }
    }
}
