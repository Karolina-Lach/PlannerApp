using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.ObjectModel;

namespace PlannerApp
{
    public class ToDoItemDb
    {
        private string connectionString;

        public ToDoItemDb()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TodoItem"].ConnectionString;
        }

        public void PobierzZBazy()
        {
            //ObservableCollection wpisyzbazy = new ObservableCollection;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var sql = @"select * from [dbo].[TableWithPlan]";

                var command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

              //          return reader; 
            
                }
            }
        }


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
                var id = (int)command.ExecuteScalar();
                

                //var id = 4;
                var sql2 = @"DELETE FROM [dbo].[TableWithPlan] WHERE Id = @Id";

                var command2 = new SqlCommand(sql2, conn);
                command2.Parameters.AddWithValue("@Id", id);
                command2.ExecuteScalar();

            }
        }


    }
}

