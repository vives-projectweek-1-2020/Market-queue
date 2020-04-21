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

namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddLocation_Click(object sender, RoutedEventArgs e)
        {
            AddLocation window = new AddLocation();
            window.Show();
            this.Close();
        }

        private void ButtonExistingLocation_Click(object sender, RoutedEventArgs e)
        {
            ExistingLocation window = new ExistingLocation();
            window.Show();
            this.Close();
        }
    }
}
