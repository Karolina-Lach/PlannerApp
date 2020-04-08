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
        DateTime currentMonday = Functions.FirstDayOfWeek(DateTime.Today).Date;   // szukanie daty poniedziałku w obecnym tygodniu
        Week week;
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            
            /* Robocze dane do planera - do wywalenia
            using (var db = new EventContext())
            {
                foreach (var entity in db.Events)
                {
                    db.Events.Remove(entity);
                    
                }
                db.SaveChanges();
                
                var Event = new EventItem(DateTime.Today, 9, 10, "Clean windows Clean windows Clean windows");
                db.Events.Add(Event);
                db.Events.Add(new EventItem(new DateTime(2020, 3, 31).Date, 9, 10, "Read a book"));
                db.Events.Add(new EventItem(new DateTime(2020, 4, 10).Date, 18, 19, "Meeting"));
                db.Events.Add(new EventItem(new DateTime(2020, 6, 6).Date, 9, 18, "Birthday"));
                db.Events.Add(new EventItem(new DateTime(2020, 6, 6).Date, 10, 18, "exam"));
                db.SaveChanges();
            }*/

            // Ładowanie obecnego tygodnia
            week = InitializeWeek(currentMonday);            

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
                MessageBox.Show("Fill all the fields, please");
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            items.Remove((TodoItem)todoList.SelectedItem);
        }

        /****************** FUNKCJE POGODY ************************/
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadInfo();
        }

        // Pobieranie informacji z API i wyświetlanie ich
        private async Task LoadInfo()
        {
            if(City.nameOfTheCity == null)
            {
                City.nameOfTheCity = "Wrocław";
            }
            var weather = await WeatherProcessor.LoadWeatherInfo(City.nameOfTheCity);
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

        // Formularz do zmiany miasta
        private async void ChangeCity_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(cityNameForm.Text))
            {
                City.nameOfTheCity = cityNameForm.Text;
                await LoadInfo();
            }
            else
            {
                MessageBox.Show("Enter the city name!");
            }
        }

        // Załaduj ponownie pogode
        private async void Reload_Click(object sender, RoutedEventArgs e)
        {
            await LoadInfo();
        }


        /*********** FUNKCJE PLANNERA ************************/

        // Tworzenie nowego TextBoxa w którym wyświetlane będzie wydarzenie
        private void CreateLabel(int Column, int Row, string Description, int span)
        {
            TextBox text = new TextBox()
            {
                TextWrapping = TextWrapping.WrapWithOverflow,
                Text = Description,
                Background = Brushes.Cornsilk,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            calendar.Children.Add(text);
            text.SetValue(Grid.RowProperty, Row);
            text.SetValue(Grid.ColumnProperty, Column);
            text.SetValue(Grid.RowSpanProperty, span);
        }

        // Wyświetlanie nowego wydarzenia w siatce kalendarza
        private void addEventToTheGrid(EventItem eventItem)
        {
            int column = DayToColumn(eventItem.Date);
            int row = HourToRow(eventItem.Beginning);
            string desc = eventItem.Name;
            int span = eventItem.End - eventItem.Beginning;

            CreateLabel(column, row, desc, span);
        }

        // Dodawnie eventów z danego tygodnia do siatki 
        private void FindEvents(Week week)
        {

            foreach (var eventItem in week.Events)
            {
                addEventToTheGrid(eventItem);
            }
        }

        // Zmiana godziny rozpoczęcia na odpowiedni rząd w siatce
        private int HourToRow(int hour)
        {
            return hour - 7;
        }

        // Zmiana dnia na odpowiednią kolumnę  w siatce
        private int DayToColumn(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            if (day > 0)
            {
                return day - 1;
            }
            else
            {
                return 6;
            }
        }

        // Inicjowanie tygodnia
        // Na podstawie poniedziałkowej daty, generowane są daty całego tygodnia i wypisywanie zdarzeń które mają miejsce w tym tygodniu
        private Week InitializeWeek(DateTime Date)
        {

            Week week = new Week(Date.Date);
            printWeekDates(week);
            FindEvents(week);
            return week;
        }

        private void printWeekDates(Week week)
        {
            dateMonday.Text = week.Monday.ToString("d.MM.yyyy");
            dateTuesday.Text = week.Tuesday.ToString("d.MM.yyyy");
            dateWednesday.Text = week.Wednesday.ToString("d.MM.yyyy");
            dateThursday.Text = week.Thursday.ToString("d.MM.yyyy");
            dateFriday.Text = week.Friday.ToString("d.MM.yyyy");
            dateSaturday.Text = week.Saturday.ToString("d.MM.yyyy");
            dateSunday.Text = week.Sunday.ToString("d.MM.yyyy");
        }

        // Następny tydzień
        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            calendar.Children.Clear();
            currentMonday = currentMonday.AddDays(7);
            InitializeWeek(currentMonday);
        }

        // Poprzedni tydzień
        private void PrevWeek_Click(object sender, RoutedEventArgs e)
        {
            calendar.Children.Clear();
            currentMonday = currentMonday.AddDays(-7);
            InitializeWeek(currentMonday);
        }

        // Dodawanie wydarzenia
        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(hourBeginnig.Text) && eventDate.SelectedDate != null
                && !string.IsNullOrWhiteSpace(hourEnd.Text) && !string.IsNullOrWhiteSpace(nameEvent.Text))
            {
                DateTime date = (DateTime)eventDate.SelectedDate;
                if (IsDateCorrect(date) && IsHourCorrect())
                {
                    EventItem task = new EventItem(date.Date, Convert.ToInt32(hourBeginnig.Text), Convert.ToInt32(hourEnd.Text), nameEvent.Text);
                    if (week.Events.Find(x => (x.Date == task.Date) && (x.Beginning == task.Beginning)) != null)
                    {
                        MessageBox.Show("You already have plans!");
                        return;
                    }
                    else
                    {
                        week.Events.Add(task);
                        addEventToTheGrid(task);
                        using (var db = new EventContext())
                        {
                            db.Events.Add(task);
                            db.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Fill all the fields!");
            }
        }

        private bool IsDateCorrect(DateTime date)
        {
            if (date.Date < currentMonday || date.Date > currentMonday.AddDays(6))
            {
                MessageBox.Show("Pick a date in a current week, please");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsHourCorrect()
        {
            int beginnig;
            int end;
            try
            {
                beginnig = Convert.ToInt32(hourBeginnig.Text);
                end = Convert.ToInt32(hourEnd.Text);

                if ((beginnig < 7 || beginnig > 18) || (end < 7 || end > 18))
                {
                    MessageBox.Show("Hours have to be between 7 and 18");
                    return false;
                }

                if (end - beginnig <= 0)
                {
                    MessageBox.Show("You can't finish the task before you started it :)");
                    return false;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Hours have to be integers");
                return false;
            }

            return true;
        }

    }
}
