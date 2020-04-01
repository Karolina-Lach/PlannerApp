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
using ApiLibrary;

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy CityForm.xaml
    /// </summary>
    public partial class CityForm : Window
    {
        public CityForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityForm.Text))
            {
                string cityName = cityForm.Text;
                City.nameOfTheCity = cityName;
            }

            this.Close();
        }
    }
}
