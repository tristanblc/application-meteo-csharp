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


namespace application_meteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string city;
        //Fonction Main
        public MainWindow()
        {
            InitializeComponent();
            SetHeaderImg();
            SetDay();
           

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
                    headerImage.Source = new BitmapImage(new Uri(@"/winter.jpg", UriKind.Relative));
                }

            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            city = cityText.Text;
            City.Content = city;
        }

        private void btnInfos_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }


}
