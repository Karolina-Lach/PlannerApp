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

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy EditPanel.xaml
    /// </summary>
    public partial class EditPanel : Window
    {
        int SelectedIndex;
        TodoItem todoItem;

        public EditPanel(int selectedIndex, TodoItem selectItem)
        {
            InitializeComponent();
            todoItem = selectItem;
            SelectedIndex = selectedIndex;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ToDoItemDb toDoItemDb = new ToDoItemDb();
            toDoItemDb.EdytujZBazy(todoItem,new TodoItem() { Description = textBox.Text, Deadline = (DateTime)NewData.SelectedDate,IsDone = false});
            this.Close();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
        }
    }
}
