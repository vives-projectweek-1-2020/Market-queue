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

        private async void CalculateCoordinates(int witchPlaceNumber)
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
                if (witchPlaceNumber == 0)
                {
                    LocationPlace0.Text = place;
                }
            }
            catch
            {
                MessageBox.Show("there has been an error parsing your data");
                
            }

        }
        private async void GetIdOfClosestAreas()
        {
            string url = "http://91.181.93.103:3040/get/area?latitude=" + userLatitude.Replace(",",".") +"&longitude="+ userLongtitude.Replace(",", ".") + "&return=area_id";
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            HttpContent content = res.Content;
            var data = await content.ReadAsStringAsync();
            closestAreaId1 = JArray.Parse(data)[0]["id"].ToString();
            closestAreaId2= JArray.Parse(data)[1]["id"].ToString();
            GetVisitorsFromAnArea(closestAreaId1);
            GetGeolocationFRomAnArea(closestAreaId1);
            GetVisitorsFromAnArea(closestAreaId2);
            GetGeolocationFRomAnArea(closestAreaId2);
            Console.WriteLine("\n closestAreaId1 " + closestAreaId1 + "          closest Areaid2" + closestAreaId2 + "\n");
        }
        private async void GetVisitorsFromAnArea(string areaId)
        {
            string url = "http://91.181.93.103:3040/get/visitor?area_id=" + areaId;
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            HttpContent content = res.Content; 
            var data = await content.ReadAsStringAsync();
            
            if (JArray.Parse(data).ToString() == "[]")
            {
                VisitorCountPlace0.Text = "There are no visitors";
            }
            Console.WriteLine("\n Amount of people in AREA0 close to you"+data+"\n");
        }
        private async void GetGeolocationFRomAnArea(string areaID)
        {
            string url = "http://91.181.93.103:3040/get/area?id=" + areaID;
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            HttpContent content = res.Content;
            var data = await content.ReadAsStringAsync();
            place0Lng = JArray.Parse(data)[0]["longitude"].ToString();
            place0Lat = JArray.Parse(data)[0]["latitude"].ToString();
            Console.WriteLine("\n longtitude: " + place0Lng + "latitude: "+ place0Lat +"\n");
            CalculateCoordinates(0);
            
        }

        private void ButtonPlace0_Click(object sender, RoutedEventArgs e)
        {
            GoingToPlace goingToPlace = new GoingToPlace();
            goingToPlace.Show();
            this.Close();
        }
    }
}
