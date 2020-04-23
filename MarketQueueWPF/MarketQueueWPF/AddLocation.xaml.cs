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
            if (NewCity.Text != "" && NewStreet.Text != "")
            {

                if (checkBox.IsChecked.Value)
                {
                    Console.WriteLine("\n latitude: " + latitude + "    longtitude:     " + longtitude + "\n");
                    //send longtitude and latitude as new place to database
                    AddAreaToDatabase(latitude.ToString().Replace(',', '.'), longtitude.ToString().Replace(',', '.'));

                }
                else
                {
                    //get the street and village name and get the coordinates from the api
                    CalculateCoordinates();
                    //then send it to the database
                }
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                text1.Text = "Please enter a valid location";
            }
        }

        private void CalculateCoordinates()
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + NewStreet.Text + "% 20" + NewCity.Text + "&key=6e978319e06444d481d5ac3f328be3ef";
                string data = "";
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    data = webClient.DownloadString(url);
                }
                var boe = JObject.Parse(data);
                string lat = boe["results"][0]["geometry"]["lat"].ToString();
                string lng = boe["results"][0]["geometry"]["lng"].ToString();

                calculatedLongtitude = lng.Replace(',', '.');
                calculatedLatitude = lat.Replace(',', '.');
                Console.WriteLine("\n calculatedLatitude: " + calculatedLatitude + "    calculatedLatitude:     " + calculatedLongtitude + "\n");
                AddAreaToDatabase(calculatedLatitude, calculatedLongtitude);
            }
            catch
            {
                MessageBox.Show("There has been an error parsing your data.");
            }

        }
        private void AddAreaToDatabase(string latitude, string longtitude)
        {
            string url = "http://91.181.93.103:3040/add/area?latitude=" + latitude + "&longitude=" + longtitude;
            string data = "";
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                data = webClient.DownloadString(url);
            }
            if(!data.StartsWith("SUCCES"))
            {
                MessageBox.Show("Something went wrong!");
            }
        }
    }
}
