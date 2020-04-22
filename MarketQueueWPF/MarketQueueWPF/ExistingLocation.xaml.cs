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
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ExistingLocation : Window
    {
        public ExistingLocation()
        {
            InitializeComponent();
            CalculateCoordinates();
        }
        double place0Lng = 3.0912;
        double place0Lat = 51.0586;

        private async void CalculateCoordinates()
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + place0Lat + "+" + place0Lng + "&key=6e978319e06444d481d5ac3f328be3ef";
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.GetAsync(url);
                HttpContent content = res.Content;
                var data = await content.ReadAsStringAsync();
                var boe = JObject.Parse(data);
                string place = boe["results"][0]["formatted"].ToString();
                Console.WriteLine("\n latitude: " +place+ "\n");
                LocationPlace0.Text = place;
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
            }

        }
    }
}
