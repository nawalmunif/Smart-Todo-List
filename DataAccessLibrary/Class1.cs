using Windows;
using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using Windows.Storage;
using System;

namespace DataAccessLibrary
{
    public class DataAccess
    {

       // public string Date { get; set; }

        public static void AddData(string inputTask, string inputDate, string inputTime, string inputCategory)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            //string dbpath = Path.Combine("C:\\Users\\hp\\AppData\\Local\\Packages", "todoproject.db");
            System.Diagnostics.Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
            //System.Diagnostics.Debug.WriteLine(inputTask);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {

                db.Open();

                SqliteCommand insertrCommand = new SqliteCommand();
                insertrCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertrCommand.CommandText = "INSERT INTO todo VALUES (NULL, @Task,@Date,@Time,@Category,NULL);";
                insertrCommand.Parameters.AddWithValue("@Task", inputTask);
                insertrCommand.Parameters.AddWithValue("@Date", inputDate);
                insertrCommand.Parameters.AddWithValue("@Time", inputTime);
                insertrCommand.Parameters.AddWithValue("@Category", inputCategory);
                //insertCommand.ExecuteNonQuery();
                insertrCommand.ExecuteReader();
                db.Close();
            }

        }
        public static void CompletedTask(string inputCompTask )
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            //string dbpath = Path.Combine("C:\\Users\\hp\\AppData\\Local\\Packages", "todoproject.db");
            System.Diagnostics.Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
            //System.Diagnostics.Debug.WriteLine(inputTask);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                
                db.Open();
                SqliteCommand CompleteCommand = new SqliteCommand();
                CompleteCommand.Connection = db;
                
                CompleteCommand.CommandText = $"UPDATE todo SET Completed='1' WHERE Task= @CompTask;";
                CompleteCommand.Parameters.AddWithValue("@CompTask", inputCompTask);
                CompleteCommand.ExecuteNonQuery();
                db.Close();

            }

        }
       

        public static void UpdateData(int inputeId, string inputeTask, string inputeDate, string inputeTime, string inputeCategory)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            //System.Diagnostics.Debug.WriteLine(dbpath);
            //System.Diagnostics.Debug.WriteLine(inputTask);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {

                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                updateCommand.CommandText = $"UPDATE todo SET Task='{inputeTask}', Date='{inputeDate}', Time='{inputeTime}', Category='{inputeCategory}' WHERE Id={inputeId};";
                /*insertCommand.Parameters.AddWithValue("@Task", inputTask);
                insertCommand.Parameters.AddWithValue("@Date", inputDate);
                insertCommand.Parameters.AddWithValue("@Time", inputTime);
                insertCommand.Parameters.AddWithValue("@Category", inputCategory);*/
                //insertCommand.ExecuteNonQuery();
                updateCommand.ExecuteReader();
                db.Close();

            }

        }

        public static void DeleteData(int delId, string delTask, string delDate, string delTime, string delCategory)
        {
            SqliteTransaction trans;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            //System.Diagnostics.Debug.WriteLine(dbpath);
            //System.Diagnostics.Debug.WriteLine(inputTask);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {

                db.Open();

                SqliteCommand delCommand = new SqliteCommand();
                delCommand.Connection = db;
                delCommand.CommandText = $"DELETE from todo where Task = @delTask;";
                delCommand.Parameters.AddWithValue("@delTask", delTask);
                // delCommand.ExecuteReader();
                delCommand.ExecuteNonQuery();

                db.Close();

            } GetTask();


        }

        public static List<String> GetTask()
        {
            List<String> taskList = new List<string>()
            { 
            };
            
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from todo", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    taskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return taskList;

        }
        public static List<String> GetCompletedTask()
        {
            List<String> CompletedTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCompletedCommand = new SqliteCommand
                    ("SELECT * from todo where Completed='1'", db);

                SqliteDataReader query = selectCompletedCommand.ExecuteReader();

                while (query.Read())
                {
                    CompletedTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return CompletedTaskList;

        }
        public static List<String> GetMeetingTask()
        {
            List<String> MeetingTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectMeetingCommand = new SqliteCommand
                    ("SELECT * from todo where Category='Meetings'", db);

                SqliteDataReader query = selectMeetingCommand.ExecuteReader();

                while (query.Read())
                {
                    MeetingTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return MeetingTaskList;

        }
        public static List<String> GetBusinessTask()
        {
            List<String> BusinessTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectBusinessCommand = new SqliteCommand
                    ("SELECT * from todo where Category='Business'", db);

                SqliteDataReader query = selectBusinessCommand.ExecuteReader();

                while (query.Read())
                {
                    BusinessTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return BusinessTaskList;
        }
        public static List<String> GetHouseholdTask()
        {
            List<String> HouseholdTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectHouseholdCommand = new SqliteCommand
                    ("SELECT * from todo where Category='Household'", db);

                SqliteDataReader query = selectHouseholdCommand.ExecuteReader();

                while (query.Read())
                {
                   HouseholdTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return HouseholdTaskList;
        }
        public static List<String> GetProjectTask()
        {
            List<String> ProjectTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectProjectCommand = new SqliteCommand
                    ("SELECT * from todo where Category='Projects'", db);

                SqliteDataReader query = selectProjectCommand.ExecuteReader();

                while (query.Read())
                {
                    ProjectTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return ProjectTaskList;
        }
        public static List<String> GetInCompletedTask()
        {
            List<String> InCompletedTaskList = new List<string>()
            {
            };

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            System.Diagnostics.Debug.WriteLine(dbpath);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCompletedCommand = new SqliteCommand
                    ("SELECT * from todo where Completed IS NULL", db);

                SqliteDataReader query = selectCompletedCommand.ExecuteReader();

                while (query.Read())
                {
                    InCompletedTaskList.Add(query.GetString(1));

                }

                db.Close();
            }
            //System.Diagnostics.Debug.WriteLine(entries[1]);
            return InCompletedTaskList;

        }

        public static List<String> getData(string inputTask)
        {
            List<String> data = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ($"SELECT Date,Time,Category,Id from todo where Task='{inputTask}'", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    for (int i = 0; i < 4; i++)
                    {
                        data.Add(query.GetString(i));
                    }
                    
                }
                db.Close();
                
               
            }
            return data;
        }
        public static void ViewData(string inputTask, string inputDate, string inputTime, string inputCategory)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            //    //string dbpath = Path.Combine("C:\\Users\\hp\\AppData\\Local\\Packages", "todoproject.db");
            System.Diagnostics.Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
            //System.Diagnostics.Debug.WriteLine(inputTask);
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {

                db.Open();

                SqliteCommand viewCommand = new SqliteCommand();
                viewCommand.Connection = db;

                viewCommand.CommandText = "SELECT Task, Date, Time, Category FROM todo;";
                
                      viewCommand.ExecuteReader();
                       db.Close();

                
            }
        }

        public static List<String> notificationGetDateTimeList()
        {
            List<String> notificationGetDateTimeList = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "todoproject.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ($"SELECT Id,Date,Time from todo", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    if (query.GetString(0) != null)
                    {
                        notificationGetDateTimeList.Add($"{query.GetString(0)}: {query.GetString(1)} {query.GetString(2)}");
                    } else
                    {
                        break;
                    }
                }
                db.Close();
            }
            return notificationGetDateTimeList;
        }
    }
}
