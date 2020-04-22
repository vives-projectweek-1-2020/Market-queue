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
using Microsoft.Maps.MapControl;
using System.Device.Location;

namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private GeoCoordinateWatcher Watcher = null;
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
        // The watcher’s status has change. See if it is ready.
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    currentLocation.Text = "Cannot find location data";
                }
                else
                {
                    currentLocation.Text = Watcher.Position.Location.Latitude.ToString();
                    currentLocation.Text += " " + Watcher.Position.Location.Longitude.ToString();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Create the watcher.

            Watcher = new GeoCoordinateWatcher();
            // Catch the StatusChanged event.

            Watcher.StatusChanged += Watcher_StatusChanged;
            // Start the watcher.

            Watcher.Start();
        }
    }
}
