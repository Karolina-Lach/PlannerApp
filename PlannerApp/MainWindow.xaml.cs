using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ApiLibrary;
using Newtonsoft.Json.Linq;

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { /// <summary>
      /// Logika interakcji dla klasy MainWindow.xaml
      /// Pierwsze podejście do Todo List
      /// Zadania są na razie są zakodowane na sztywno -- póżniej powinny być wczytywane z bazy danych
      /// Można dodać zadanie i usunąć wybrane zadanie
      /// </summary>
        ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();

            items.Add(new TodoItem() { Description = "Complete this project", Deadline = new DateTime(2020, 5, 1), IsDone = false });
            items.Add(new TodoItem() { Description = "Review exam material", Deadline = new DateTime(2020, 6, 1), IsDone = false });
            items.Add(new TodoItem() { Description = "Plan a party", Deadline = new DateTime(2020, 4, 10), IsDone = false });

            todoList.ItemsSource = items;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(descirptionBox.Text) && deadlineBox.SelectedDate != null)
            {
                items.Add(new TodoItem() { Description = descirptionBox.Text, Deadline = (DateTime)deadlineBox.SelectedDate, IsDone = false });
                todoList.ItemsSource = items;
            }
            else
            {
                // błąd -- obsługa póżniej 
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            items.Remove((TodoItem)todoList.SelectedItem);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadInfo();
        }

        private async Task LoadInfo(string name = "Wrocław")
        {
            var weather = await WeatherProcessor.LoadWeatherInfo(name);
            double temp = weather.main.temp;
            int celsius = (int)(temp - 273.15);
            temperatureText.Text = $"{ celsius } ℃";

            Weather[] array = weather.weather.ToArray();

            descText.Text = array[0].description;

            string img = $"http://openweathermap.org/img/wn/{ array[0].icon }@2x.png";
            var uriSource = new Uri(img, UriKind.Absolute);
            weatherImage.Source = new BitmapImage(uriSource);

            City.nameOfTheCity = weather.name;
            cityName.Text = City.nameOfTheCity;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CityForm cityForm = new CityForm();

            cityForm.Show();

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await LoadInfo(City.nameOfTheCity);
        }
    }
}
