using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using System.Windows;

namespace SmartToDoList
{

    class TaskData
    {
        public int id { get; set; }
        public string category { get; set; }
        public string task { get; set; }
        public string deadlineDate { get; set; }
        public string priority { get; set; }
        public string deadlineTime { get; set; }
        }
    class Status
    {
        public int completed { get; set; }
    }
       
}
