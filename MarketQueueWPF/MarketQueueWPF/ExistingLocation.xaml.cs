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
using System.Net;


namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ExistingLocation : Window
    {
        public ExistingLocation(string userLatitude, string userLongtitude)
        {
            InitializeComponent();
            this.userLatitude = userLatitude;
            this.userLongtitude = userLongtitude;
            GetIdOfClosestAreas(userLatitude, userLongtitude);
        }
        string place0Lng = "";
        string place0Lat = "";
        string userLatitude = "";
        string userLongtitude = "";

        private string CalculateCoordinates()
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + place0Lat + "+" + place0Lng + "&key=6e978319e06444d481d5ac3f328be3ef";
                var data = SendAndRequestData(url);
                var boe = JObject.Parse(data);
                string place = boe["results"][0]["formatted"].ToString();
                return place;
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
                return "";
            }

        }
        public class Area
        {
            public string Area_Name { get; set; }
            public string visitors { get; set; }
            public Button button { get; set; }

        }
        private void GetIdOfClosestAreas(string latitude, string longtitude)
        {
            try
            {
                
                string url = "http://91.181.93.103:3040/get/area?latitude=" + latitude.Replace(",", ".") + "&longitude=" + longtitude.Replace(",", ".") + "&return=area_id";
                var data = SendAndRequestData(url);
                Console.WriteLine("\n data get closest areas: " + data + "\n");
                List<Area> items = new List<Area>();
                Areas.ItemsSource = items;
                
                for (int i = 0; i < JArray.Parse(data).Count; i++)
                {
                    Console.WriteLine("\n Area_name " + GetGeolocationFRomAnArea(JArray.Parse(data)[i]["id"].ToString()) + "          visitors: " + GetVisitorsFromAnArea(JArray.Parse(data)[i]["id"].ToString() + "\n"));
                   
                    items.Add(new Area() { Area_Name = GetGeolocationFRomAnArea(JArray.Parse(data)[i]["id"].ToString()), visitors = GetVisitorsFromAnArea(JArray.Parse(data)[i]["id"].ToString()) }) ;
                    Areas.ItemsSource = items;
                    
                }
                
                //AmountOfAreaIDs = JArray.Parse(data).Count;
            }
            catch
            {

            }


        }
        private string GetVisitorsFromAnArea(string areaId)
        {
            string url = "http://91.181.93.103:3040/get/visitor?area_id=" + areaId + "&all=true&return=count";
            var data = SendAndRequestData(url);
            var rawData = JArray.Parse(data)[0]["total"].ToString();
            Console.WriteLine("\n Amount of people in AREA0 close to you" + data + "\n");
            return rawData;
        }
        private string CalculateLatitude(string city, string street)
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + street + "%20" + city + "&key=6e978319e06444d481d5ac3f328be3ef";
                var data = SendAndRequestData(url);
                var boe = JObject.Parse(data);
                string lat = boe["results"][0]["geometry"]["lat"].ToString();
                string lng = boe["results"][0]["geometry"]["lng"].ToString();
                Console.WriteLine("\n Latitude: " + lat + "\n");

                return lat.Replace(',', '.');
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
                return "";
            }

        }
        private string CalculateLongtitude(string city, string street)
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + street + "%20" + city + "&key=6e978319e06444d481d5ac3f328be3ef";
                var data = SendAndRequestData(url);
                var boe = JObject.Parse(data);
                string lat = boe["results"][0]["geometry"]["lat"].ToString();
                string lng = boe["results"][0]["geometry"]["lng"].ToString();
                return lng.Replace(',', '.');
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
                return "";
            }

        }
        private string GetGeolocationFRomAnArea(string areaID)
        {
            string url = "http://91.181.93.103:3040/get/area?id=" + areaID;
            var data = SendAndRequestData(url);
            place0Lng = JArray.Parse(data)[0]["longitude"].ToString();
            place0Lat = JArray.Parse(data)[0]["latitude"].ToString();
            Console.WriteLine("\n longtitude: " + place0Lng + "latitude: " + place0Lat + "\n");
            return CalculateCoordinates();

        }
        private string SendAndRequestData(string url)
        {
            string content = "";
            using (WebClient webClient = new WebClient())
            {
                content = webClient.DownloadString(url);
            }
            return content;
        }


        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GoingToPlace goingToPlace = new GoingToPlace();
            goingToPlace.Show();
            this.Close();
        }

        private void Sercher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------");

                GetIdOfClosestAreas(CalculateLatitude(SercherCity.Text, Sercher.Text),CalculateLongtitude(SercherCity.Text, Sercher.Text));
                Console.WriteLine("------------------------------------------------------------------------------------------------");
            }
        }

        private void Sercher_GotFocus(object sender, RoutedEventArgs e)
        {
            Sercher.Text = "";
        }

        private void SercherCity_GotFocus(object sender, RoutedEventArgs e)
        {
            SercherCity.Text = "";

        }
    }
}
