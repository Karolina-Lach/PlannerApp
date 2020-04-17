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
    /// Główna klasa <c>MainWindow</c>
    /// Zawiera wszystkie metody odpowiedzialne za wyświetlanie elementów w głównym oknie aplikacji
    /// <list type="bullet">
    /// <item>
    ///     <term> Main Window </term>
    ///     <description>Inicjalizacja elementów</description>
    /// </item>
    /// <item>
    ///     <term>AddItem_Click</term>
    ///     <description>Dodanie elementu do listy Todo</description>
    /// </item>
    /// <item>
    ///     <term>RemoveItem_Click</term>
    ///     <description>Usuwnie elementu z listy Todo</description>
    /// </item>
    /// <item>
    ///     <term>Window_Loaded</term>
    ///     <description>Ładowanie pobranych danych z API</description>
    /// </item>
    /// <item>
    ///     <term>LoadInfo</term>
    ///     <description>Wyodrębnienie potrzebnych danych z pliku JSON</description>
    /// </item>
    /// <item>
    ///     <term>ChangeCity_Click</term>
    ///     <description>Zmiana miasta</description>
    /// </item>
    /// <item>
    ///     <term>Reload_Click </term>
    ///     <description>Ponowne pobranie danych z API</description>
    /// </item>
    /// <item>
    ///     <term>CreateTextBoxEvent</term>
    ///     <description>Tworzenie TextBoxu do wyświetlania wydarzenia</description>
    /// </item>
    /// <item>
    ///     <term>AddEventToTheGrid</term>
    ///     <description>Dodanie wydarzenia do siatki kalendarza</description>
    /// </item>
    /// <item>
    ///     <item>AddEventsInWeek</item>
    ///     <description>Dodanie wydarzeń z wybranego tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>InitializeWeek</term>
    ///     <description>Inicjalizacja tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>PrintWeekDates</term>
    ///     <description>Wyświetlanie dat z tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>NextWeek_Click</term>
    ///     <description>Przejscie do następnego tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>PrevWeek_Click</term>
    ///     <description>Przejscie do poprzedniego tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>AddEvent_Click</term>
    ///     <description>Dodanie nowego wydarzenia</description>
    /// </item>
    /// <item>
    ///     <term>IsDateCorrect</term>
    ///     <description>Sprawdzenie poprawności daty</description>
    /// </item>
    /// <item>
    ///     <term>IsHourCorrect</term>
    ///     <description>Sprawdzenie poprawności godziny</description>
    /// </item>
    /// </list>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    { 
        ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();
        DateTime currentMonday = Functions.FirstDayOfWeek(DateTime.Today).Date;   // szukanie daty poniedziałku w obecnym tygodniu
        Week week;

        
        /// <summary>
        /// Funkcja inicjująca elementy w Main Window
        /// </summary>
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


        /// <summary>
        /// Dodaje nowy element do Todo list po naciśnięciu przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Jeśli TextBox nie jest pusty i data została wybrana, dodaje nowy element do listy.
        /// Jeśli TextBox jest pusty albo data nie została wybrana, wyświetla komunikat. 
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


        /// <summary>
        /// Usuwa zaznaczony element z Todo List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            items.Remove((TodoItem)todoList.SelectedItem);
        }

        /****************** FUNKCJE POGODY ************************/

        /// <summary>
        /// Ładuje i wyświetla informacje pobrane z zewnętrznego systemu API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Zobacz <see cref="MainWindow.LoadInfo"/> do pobierania informacji z API/>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadInfo();
        }

        
        /// <summary>
        /// Wyodrębnia potrzebne dane z pliku JSON
        /// </summary>
        /// <returns></returns>
        /// Zobacz <see cref="WeatherProcessor.LoadWeatherInfo(string)"/> do połączeniz systemem API
        
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

        
        /// <summary>
        /// Po wciśnięciu przycisku "Change City" zmienia miasto, o którym wyświetlane są informacje pogodowe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Jeśli TextBox nie jest pusty, zmienia nazwę miasta i ponownie łączy się z API
        /// Jeśli TextBox jest pusty, wyświetla odpowiedni komunikat
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

        
        /// <summary>
        /// Po wciśnieciu przycisku Reload, ponownie ładuje informacje o pogodzie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Reload_Click(object sender, RoutedEventArgs e)
        {
            await LoadInfo();
        }


        /*********** FUNKCJE PLANNERA ************************/
        /// <summary>
        /// Tworzy nowy TextBox, w którym zostanie wypisane nowe wydarzenie w kalendarzu
        /// </summary>
        /// <param name="Column">Numer kolumny w siatce kalendarza, w której ma zostać utworzony nowy TextBox</param>
        /// <param name="Row">Numer wiersze w siatce kalendarza, w której ma zostać utworzony nowy TextBox</param>
        /// <param name="Description">Nazwa wydarzenia wyświetlana na ekranie</param>
        /// <param name="span">Ile wierszy (godzin) zajmuje wydarzenie </param>
        /// Zobacz <see cref="MainWindow.AddEventToTheGrid(EventItem)"/>
        private void CreateTextBoxEvent(int Column, int Row, string Description, int span)
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

        
        /// <summary>
        /// Dodaje wydarzenie do siatki kalendarza 
        /// </summary>
        /// <param name="eventItem">Wydarzenie, które ma zostać wyświetlone </param>
        /// Zobacz <see cref="MainWindow.AddEventsInWeek(Week)"/>
        private void AddEventToTheGrid(EventItem eventItem)
        {
            int column = Functions.DayToColumn(eventItem.Date);
            int row = Functions.HourToRow(eventItem.Beginning);
            string desc = eventItem.Name;
            int span = eventItem.End - eventItem.Beginning;

            CreateTextBoxEvent(column, row, desc, span);
        }

        /// <summary>
        /// Dodaje wszystkie wydarzenia z danego tygodnia do siatki kalendarza 
        /// </summary>
        /// <param name="week">Aktualnie wyświetlany tydzień </param>
        /// Zobacz <see cref="MainWindow.InitializeWeek(DateTime)"/>
        private void AddEventsInWeek(Week week)
        {
            foreach (var eventItem in week.Events)
            {
                AddEventToTheGrid(eventItem);
            }
        }

        /// <summary>
        /// Inicjowanie całego tygodnia
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        /// Na podstawie poniedziałkowej daty, generowane są daty całego tygodnia i wypisywane zdarzenia, które mają miejsce w tym tygodniu
        private Week InitializeWeek(DateTime Date)
        {
            Week week = new Week(Date.Date);
            PrintWeekDates(week);
            AddEventsInWeek(week);
            return week;
        }


        /// <summary>
        /// Wyświetlania dat całego tygodnia w nagłówkach kolumn kalendarza
        /// </summary>
        /// <param name="week">Aktualnie wyświetlany tydzień</param>
        /// Zobacz <see cref="MainWindow.InitializeWeek(DateTime)"/>
        private void PrintWeekDates(Week week)
        {
            dateMonday.Text = week.Monday.ToString("d.MM.yyyy");
            dateTuesday.Text = week.Tuesday.ToString("d.MM.yyyy");
            dateWednesday.Text = week.Wednesday.ToString("d.MM.yyyy");
            dateThursday.Text = week.Thursday.ToString("d.MM.yyyy");
            dateFriday.Text = week.Friday.ToString("d.MM.yyyy");
            dateSaturday.Text = week.Saturday.ToString("d.MM.yyyy");
            dateSunday.Text = week.Sunday.ToString("d.MM.yyyy");
        }

        
        /// <summary>
        /// Po kliknięciu w przycisk NextWeek, wyświetla kolejny tydzień
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Czyści siatkę kalendarza, aktualizuję datę aktualnego poniedziałku i inicjuje nowy tydzień
        /// Zobacz <see cref="MainWindow.InitializeWeek(DateTime)"/>, żeby zobaczyć inicjowanie nowego tygodnia
        /// Zobacz także <seealso cref="MainWindow.PrevWeek_Click(object, RoutedEventArgs)"/>
        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            calendar.Children.Clear();
            currentMonday = currentMonday.AddDays(7);
            InitializeWeek(currentMonday);
        }

        /// <summary>
        /// Po kliknięciu w przycisk PrevWeek, wyświetla poprzedni tydzień
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Czyści siatkę kalendarza, aktualizuję datę aktualnego poniedziałku i inicjuje nowy tydzień
        /// Zobacz <see cref="MainWindow.InitializeWeek(DateTime)"/>, żeby zobaczyć inicjowanie nowego tygodnia
        /// Zobacz także <seealso cref="MainWindow.NextWeek_Click(object, RoutedEventArgs)"/>
        private void PrevWeek_Click(object sender, RoutedEventArgs e)
        {
            calendar.Children.Clear();
            currentMonday = currentMonday.AddDays(-7);
            InitializeWeek(currentMonday);
        }

        
        /// <summary>
        /// Po kliknięciu w przycisk AddEvent dodaje nowe wydarzenie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Sprawdza czy wszystkie pola są wypełnione. Jeśli nie są, wyświetlany jest odpowiedni komunikat.
        /// Jeśli wybrana data nie znajduje się w aktualnie wyświetlanym tygodniu, wyświetlany jest odpowiedni komunikat.
        /// Sprawdzana jest poprawność wybranej daty i wprowadzonej godziny. Zobacz <see cref="MainWindow.IsDateCorrect(DateTime)"/> i <see cref="MainWindow.IsHourCorrect"/>
        /// Jeśli w wybranym dniu i o wybranej godzinie jest już dodane wydarzenie, wyświetalny jest odpowiedni komunikat.
        /// Zobacz <see cref="MainWindow.AddEventToTheGrid(EventItem)"/>, żeby zobaczyć dodawanie wydarzenia do siatki kalendarza
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
                        AddEventToTheGrid(task);
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


        /// <summary>
        /// Sprawdza czy wybrana data wydarzenia jest prawidłowa
        /// </summary>
        /// <param name="date">Data wybrana przez użytkownika</param>
        /// <returns>Zwraca wartość bool, w zależności od tego, czy data jest prawidłowa </returns>
        /// Data jest prawidłowa jeśli znajduje się w wyświetlanym obecnie tygodniu
        /// Zobacz <see cref="MainWindow.AddEvent_Click(object, RoutedEventArgs)"/>
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


        /// <summary>
        /// Sprawdza czy wpisane godziny są prawidłowe
        /// </summary>
        /// <returns>Zwraca wartość bool, w zależności od tego, czy godziny są prawidłowe</returns>
        /// Jeśli wpisane godziny nie są pomiędzy 7 a 18 - wyświetla odpoweidni komunikat i zwraca false
        /// Jeśli koniec wydarzenia jest przed początkiem - wyświetla odpoweidni komunikat i zwraca false
        /// <exception cref="FormatException">Wyrzucany jeśli wpisane wartości nie są typu Integer</exception>
        /// Zobacz <see cref="MainWindow.AddEvent_Click(object, RoutedEventArgs)"/>
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
