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
    }
}
