using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace PlannerApp
{
    public class ToDoItemDb
    {
        private string connectionString;

        public ToDoItemDb()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TodoItem"].ConnectionString;
        }

	//Funkcja która pobiera z bazy danych wszystkie wpisy, dodaje je do ObservableCollection<TodoItem>
	// i zwraca po przeczytaniu wszystkich rekordów z bazy. 
        public ObservableCollection<TodoItem> PobierzZBazy()
        {
            ObservableCollection<TodoItem> wpisyzbazy = new ObservableCollection<TodoItem>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var sql = @"select Description,Deadline, IsDone from [dbo].[TableWithPlan]";

                var command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TodoItem todoItem;
                        wpisyzbazy.Add(
                        todoItem = new TodoItem
                        {
                            Description = reader["Description"] as string,
                            Deadline =(DateTime) reader["Deadline"],
                            IsDone = (bool)reader["IsDone"]
                        });
                    }
                    return wpisyzbazy; 
                }
            }
        }

	// Funkcja wysyłająca do bazy danych wpiy z dodawanymi danymi
        public void WyslijDoBazy(string Description, DateTime dateTime, bool IsDone)
        {
            using(var conn = new SqlConnection(connectionString))
           {
                conn.Open();
                var sql = @"INSERT INTO [dbo].[TableWithPlan](Description,Deadline,IsDone) VALUES 
                   (@Description,@DateTime,@IsDone)";
                     
                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@DateTime", dateTime);
                command.Parameters.AddWithValue("@IsDone", IsDone);
                command.ExecuteReader();
            }
        }
	
	// Funkcja usuwająca rekordy z bazy danych. Pierwsza komenda SQL pobiera Id usuwanego elementu a druga 
	// usuwa rekord o danym Id z bazy.
        public void UsunZBazy(TodoItem todoItem)
        {
            var dealine = todoItem.Deadline;
            var description = todoItem.Description;
            var isDone = todoItem.IsDone;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var sql = @"SELECT Id FROM [dbo].[TableWithPlan] WHERE " +
                "Description = @Description AND Deadline = @DateTime AND IsDone = @IsDone";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@Description", (System.Data.SqlTypes.SqlString) description);
                command.Parameters.AddWithValue("@DateTime", (System.Data.SqlTypes.SqlDateTime) dealine);
                command.Parameters.AddWithValue("@IsDone", (System.Data.SqlTypes.SqlBoolean) isDone);
                int id = (int)command.ExecuteScalar();
                
                var sql2 = @"DELETE FROM [dbo].[TableWithPlan] WHERE Id = @Id";
                var command2 = new SqlCommand(sql2, conn);
                command2.Parameters.AddWithValue("@Id", id);
                command2.ExecuteScalar();

            }
        }
    }
}

