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
using System.Device.Location;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Window
    {
        private double latitude = 0;
        private double longtitude = 0;
        private string calculatedLatitude = "";
        private string calculatedLongtitude = "";


        public AddLocation(double latitude, double longtitude)
        {
            InitializeComponent();
            this.longtitude = longtitude;
            this.latitude = latitude;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked.Value)
            {
                Console.WriteLine("\n latitude: " + latitude + "    longtitude:     " + longtitude + "\n");
                //send longtitude and latitude as new place to database
            }
            else
            {
                //get the street and village name and get the coordinates from the api
                CalculateCoordinates();

               //then send it to the database
            }
        }

        private  async void CalculateCoordinates()
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + NewStreet.Text + "% 20" + NewCity.Text + "&key=6e978319e06444d481d5ac3f328be3ef";
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.GetAsync(url);
                HttpContent content = res.Content;
                var data = await content.ReadAsStringAsync();
                var boe = JObject.Parse(data);
                string lat = boe["results"][0]["geometry"]["lat"].ToString();
                string lng = boe["results"][0]["geometry"]["lng"].ToString();
                
                calculatedLongtitude = lng.Replace(',','.');
                calculatedLatitude = lat.Replace(',', '.');
                Console.WriteLine("\n calculatedLatitude: " + calculatedLatitude + "    calculatedLatitude:     " + calculatedLongtitude + "\n");
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
            }

        }
    }
}
