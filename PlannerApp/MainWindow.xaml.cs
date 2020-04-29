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
        ToDoItemDb toDoItemDb = new ToDoItemDb();     // tworzenie instacji klasy obsługującej bazę danych

        public MainWindow()
        {
            InitializeComponent();

            items = toDoItemDb.loadFromDataBase();  // funkcja która pobiera z bazy 

            todoList.ItemsSource = items;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(descirptionBox.Text) && deadlineBox.SelectedDate != null)
                {
                    items.Add(new TodoItem() { Description = descirptionBox.Text, Deadline = (DateTime)deadlineBox.SelectedDate, IsDone = false });
                    todoList.ItemsSource = items;
                    toDoItemDb.sendToDataBase(descirptionBox.Text, deadlineBox.SelectedDate.Value, false);  // wywołanie funkcji dodającej wpisy do bazy 
                    descirptionBox.Text = "";
                    deadlineBox.SelectedDate = null;
                }
                else
                    throw new ArgumentNullException("Null Argument");
            }
            catch
            {
                if (string.IsNullOrWhiteSpace(descirptionBox.Text) && deadlineBox.SelectedDate == null)
                    MessageBox.Show("Wpisz nazwę wydarzenia i wybierz datę wydarzenia");
                else if (string.IsNullOrWhiteSpace(descirptionBox.Text))
                    MessageBox.Show("Wpisz nazwę wydarzenia");
                else if (deadlineBox.SelectedDate == null)
                    MessageBox.Show("Wybierz datę wydarzenia");

            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {                    
                int selectedIndex = todoList.SelectedIndex;
                Object selectItem = todoList.SelectedItem;
                if(selectedIndex!=0)
                {
                    items.Remove((TodoItem)selectItem);
                    toDoItemDb.UsunZBazy((TodoItem)selectItem);  //  wywołanie funkcji usuwającej wpisy z bazy 
                }
                else
                    throw new ArgumentNullException("NullArgument");
            }
            catch
            {
                MessageBox.Show("Wybierz element do usunęcia");
            }
        }
    }
}
