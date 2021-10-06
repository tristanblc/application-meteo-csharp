using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json.Linq;

namespace application_meteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string baseApi = "https://goweather.herokuapp.com/weather/Paris";
        public string baseApiNews1 = "https://newsapi.org/v2/everything?q=";
        public string baseApiNews2 = "&from=2021-10-06&sortBy=popularity&apiKey=";
        public string city ="paris";
        public string json;

        public string jsonString;
        //Fonction Main
        public MainWindow()
        {
            InitializeComponent();
            //SetHeaderImg();
            SetDay();
            GetApiResponse();
            LoadNews();



        }

        //Fonction API/JSON
        public void GetApiResponse()
        {
            using(WebClient wc = new WebClient())
            {
                jsonString = wc.DownloadString(baseApi+city);
                //Encodage UTF - 8
                byte[] bytes = Encoding.Default.GetBytes(jsonString);

                jsonString = Encoding.UTF8.GetString(bytes);
                SetUiInfos();
               
            }
        }

      
        public void SetUiInfos()
        {
            JObject o = JObject.Parse(jsonString);
            //MessageBox.Show(jsonString + city);
            weatherDesc.Content = o["description"];
            if (o["description"].ToString().Contains("sunny"))
            {
                headerImage.Source = new BitmapImage(new Uri(@"/sunset.jpg", UriKind.Relative));
            }
            if (o["description"].ToString().Contains("rain"))
            {
                headerImage.Source = new BitmapImage(new Uri(@"/rain.jpg", UriKind.Relative));
            }
            if (o["description"].ToString().Contains("winter"))
            {
                headerImage.Source = new BitmapImage(new Uri(@"/winter.jpg", UriKind.Relative));
            }
            Rafale.Content = o["wind"];
            Temperature.Content = o["temperature"];
            forecast0_temp.Content = o["forecast"][0]["temperature"];
            forecast1_temp.Content = o["forecast"][1]["temperature"];
            forecast2_temp.Content = o["forecast"][2]["temperature"];
          
            }

        //Fonctions Articles
        public void LoadNews()
        {
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(baseApiNews1 + city + baseApiNews2);
                //Encodage UTF - 8
                byte[] bytes = Encoding.Default.GetBytes(json);

                json= Encoding.UTF8.GetString(bytes);
    
            
            }
            JObject o = JObject.Parse(json);
      
            List<Article> lesArticles = new List<Article>();
            for (int i = 0; i < o["articles"].Count(); i++)
            {
                string articleTitle = o["articles"][i]["title"].ToString();
         
                lesArticles.Add(new Article() { Titre = articleTitle});
            }

            articlesListes.ItemsSource = lesArticles;
         }
  
        //Nos fonctions
        public void SetDay()
        {
            string date = CapitalazeStr(DateTime.Now.ToString("dddd,dd MMMM yyyy"));
            Date.Content = date;

            forecast0.Content = CapitalazeStr(DateTime.Now.AddDays(1).ToString("ddd"));
            forecast1.Content = CapitalazeStr(DateTime.Now.AddDays(2).ToString("ddd"));
            forecast2.Content = CapitalazeStr(DateTime.Now.AddDays(3).ToString("ddd"));
        }


        public string CapitalazeStr(string str)
        {
            string maString = str;
            maString = char.ToUpper(str[0]) + str.Substring(1);
            return maString;
        }

        public void SetHeaderImg()
        {
            string meteo = "sun";
            if (meteo == "pluie")
            {
                headerImage.Source = new BitmapImage(new Uri(@"/rain.jpg", UriKind.Relative));
            }
            else
            {
                if (meteo == "sun")
                {
                    headerImage.Source = new BitmapImage(new Uri(@"/sunset.jpg", UriKind.Relative));
                }
                if (meteo == "winter")
                {
                    
                }

            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            city = cityText.Text;
            City.Content = city;
            GetApiResponse();
            SetUiInfos();
            LoadNews();
        }

  
       
    }


}


public class Article {

    private string titre;

    public string Titre { get => titre; set => titre = value; }

    public override string ToString()
    {
        return base.ToString();
    }
}