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
            GetIdOfClosestAreas();
        }
        string place0Lng = "";
        string place0Lat = "";
        string userLatitude = "";
        string userLongtitude = "";
        string closestAreaId1 = "";
        string closestAreaId2 = "";
        string closestAreaId3 = "";
        string closestAreaId4 = "";
        string closestAreaId5 = "";
        string closestAreaId6 = "";
        int AmountOfAreaIDs = 0;

        private string CalculateCoordinates(int witchPlaceNumber)
        {
            try
            {
                string url = "https://api.opencagedata.com/geocode/v1/json?q=" + place0Lat + "+" + place0Lng + "&key=6e978319e06444d481d5ac3f328be3ef";
                var data = SendAndRequestData(url);
                var boe = JObject.Parse(data);
                string place = boe["results"][0]["formatted"].ToString();
                Console.WriteLine("\n latitude: " + place + "\n");
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
        private void GetIdOfClosestAreas()
        {
            try
            {
                string url = "http://91.181.93.103:3040/get/area?latitude=" + userLatitude.Replace(",", ".") + "&longitude=" + userLongtitude.Replace(",", ".") + "&return=area_id";
                var data = SendAndRequestData(url);
                closestAreaId1 = JArray.Parse(data)[0]["id"].ToString();
                List<Area> items = new List<Area>();
                //items.Add(new Area() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
                // VisitorCountPlace0.Text = GetVisitorsFromAnArea(closestAreaId1);
                // LocationPlace0.Text = GetGeolocationFRomAnArea(closestAreaId1);
                for (int i = 0; i < JArray.Parse(data).Count; i++)
                {
                    Console.WriteLine("\n Area_name " + GetGeolocationFRomAnArea(JArray.Parse(data)[i]["id"].ToString()) + "          visitors: " + GetVisitorsFromAnArea(JArray.Parse(data)[i]["id"].ToString() + "\n"));
                    Button btn = new Button();
                    btn.Content = "Jo";
                    items.Add(new Area() { Area_Name = GetGeolocationFRomAnArea(JArray.Parse(data)[i]["id"].ToString()), visitors = GetVisitorsFromAnArea(JArray.Parse(data)[i]["id"].ToString()), button = btn }) ;
                    Areas.ItemsSource = items;
                }
                AmountOfAreaIDs = JArray.Parse(data).Count;
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
        private string GetGeolocationFRomAnArea(string areaID)
        {
            string url = "http://91.181.93.103:3040/get/area?id=" + areaID;
            var data = SendAndRequestData(url);
            place0Lng = JArray.Parse(data)[0]["longitude"].ToString();
            place0Lat = JArray.Parse(data)[0]["latitude"].ToString();
            Console.WriteLine("\n longtitude: " + place0Lng + "latitude: " + place0Lat + "\n");
            return CalculateCoordinates(0);

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

        private void ButtonPlace0_Click(object sender, RoutedEventArgs e)
        {
            GoingToPlace goingToPlace = new GoingToPlace();
            goingToPlace.Show();
            this.Close();
        }
    }
}
