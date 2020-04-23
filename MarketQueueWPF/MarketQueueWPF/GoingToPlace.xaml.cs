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
using System.Net;

namespace MarketQueueWPF
{
    /// <summary>
    /// Interaction logic for GoingToPlace.xaml
    /// </summary>
    public partial class GoingToPlace : Window
    {
        public GoingToPlace(string id)
        {
            InitializeComponent();
            this.id = id;
        }
        string id = "";
        private string SendAndRequestData(string url)
        {
            string content = "";
            using (WebClient webClient = new WebClient())
            {
                content = webClient.DownloadString(url);
            }
            return content;
        }
        private void VisitTime_GotFocus(object sender, RoutedEventArgs e)
        {
            VisitTime.Text = "";
        }
        private void OffsetTime_GotFocus(object sender, RoutedEventArgs e)
        {
            OffsetTime.Text = "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int offset = Convert.ToInt32(OffsetTime.Text);
                int duration = Convert.ToInt32(VisitTime.Text);
                if(offset<0 || duration <= 0)
                {
                    throw new FormatException();
                }

                string url = "http://91.181.93.103:3040/add/visitor?area_id=" + id + "&duration=" + duration + "&offset=" + offset;
                string answer = SendAndRequestData(url);
                if (!answer.StartsWith("SUCCESS"))
                {
                    MessageBox.Show("Something went wrong!");
                }
                this.Close();
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input!");
            }
        }
    }
}
