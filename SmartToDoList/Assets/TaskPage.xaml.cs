using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Notifications;

//https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/dialogs-and-flyouts/dialogs

namespace SmartToDoList
{

    public partial class TaskPage : Page
    {
        public string taskQuery { get; set; }
        public DateTime selectDateEdit { get; set; }

        public TaskPage()
        {
            this.InitializeComponent();
            WelcomeVoiceTaskPage(); //UNCOMMENT LATER
            generatingList();
            //System.Diagnostics.Debug.WriteLine(DataAccess.notificationGetDateTimeList().Count);
            //notificationAlert(); NOTIFICATION NOT WORKING
        }
        ObservableCollection<TaskData> taskItems = new ObservableCollection<TaskData>();

        TaskData t1 = new TaskData();
        notificationData notificationData = new notificationData();
        public void generatingList()
        {
            //TaskList.ItemsSource = null;
            TaskList.ItemsSource = DataAccess.GetTask();

        }
        //string dt = (t1.deadlineDate, t1.deadlineTime);
        //DateTime newdt = DateTime.Parse(dt);
        public void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            t1.task = TaskBox.Text;
            if (TaskBox.Text.Trim() == string.Empty)
            {
                InvalidPopup.IsOpen = true;
            }
            else
            {
                DataAccess.AddData(t1.task, t1.deadlineDate, t1.deadlineTime, t1.category);

                if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
                taskItems.Add(t1);

                generatingList();
                this.Frame.Navigate(typeof(Navigate));
            }
            //ResetTask();
        }

        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)

        {
            //this.InitializeComponent();

            //ResetTask();

            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }

            EnterButton.Visibility = Visibility.Visible;
            //UpdateButton.Visibility = Visibility.Collapsed;

        }


        public void CategoryTaskDefine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            t1.category = item.Content.ToString();
            //var Category = t1.category;}

        }

        //private void PriorityDefine_Click(object sender, RoutedEventArgs e)
        //{
        //    t1.priority = PriorityDefine.IsChecked.ToString();

        //}

        private void DeadlineTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            try
            {
                t1.deadlineTime = DeadlineTime.Time.ToString();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("A handled exception just occurred");
            }

        }

        private void DeadlineDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var dt = DeadlineDate.Date;
            t1.deadlineDate = dt.Value.Date.ToShortDateString();

        }

        public void ResetTask()
        {
            t1.id = 0;
            TaskBox.Text = "";
            DeadlineDate.Date = default(DateTimeOffset);
            DeadlineTime.Time = default;
            //CategoryTaskDefine = PriorityDefine_Click(default);

        }

        public void getDataDB()
        {
            List<string> data = DataAccess.getData(TaskBox.Text);
            t1.id = Int32.Parse(data[3]);
            DateTime oDate = Convert.ToDateTime(data[0]);
            DeadlineDate.Date = oDate;
            TimeSpan oTime = TimeSpan.Parse(data[1]);
            DeadlineTime.Time = oTime;
            //var Categories = t1.category;
            System.Diagnostics.Debug.WriteLine(t1.id);


        }
        private void GotoCompleted_Task(object sender, RoutedEventArgs e)
        {


            //DataAccess.CompleteTask(t1.task);
            this.Frame.Navigate(typeof(Pending_completed));
        }
        private void GotoCategory_Task(object sender, RoutedEventArgs e)
        {


            //DataAccess.CompleteTask(t1.task);
            this.Frame.Navigate(typeof(Categories));
        }
        private void Completed_Task(object sender, RoutedEventArgs e)
        {


            DataAccess.CompletedTask(t1.task);
            ViewPopup.IsOpen = false;

        }
        private void CloseInvalidpopup(object sender, RoutedEventArgs e)
        {



            InvalidPopup.IsOpen = false;

        }
        private void GotoIncomplete_Task(object sender, RoutedEventArgs e)
        {


            //DataAccess.CompleteTask(t1.task);
            this.Frame.Navigate(typeof(IncompleteTask));
        }
        private void deletedTask(object sender, RoutedEventArgs e)
        {


            DataAccess.DeleteData(t1.id, t1.task, t1.deadlineDate, t1.deadlineTime, t1.category);
            DeletePopup.IsOpen = false;
            ViewPopup.IsOpen = false;
            this.Frame.Navigate(typeof(Navigate));
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            t1.task = EditTaskBox.Text;

            DataAccess.UpdateData(t1.id, t1.task, t1.deadlineDate, t1.deadlineTime, t1.category);
            EditStandardPopup.IsOpen = false;
            System.Diagnostics.Debug.WriteLine(TaskList.ItemsSource);
            ResetTask();
            this.Frame.Navigate(typeof(Navigate));

            //generatingList(); //UPDATING DATA ON RUN-TIME (NOT WORKING) DUE TO line 117 IDKW
        }


        private void ShowPopupOffsetClickedDelete(object sender, RoutedEventArgs e)
        {
            if (!DeletePopup.IsOpen) { DeletePopup.IsOpen = true; }

        }
        private void CloseViewpopup(object sender, RoutedEventArgs e)
        {
            ViewPopup.IsOpen = false;

        }
        private void CloseEditpopup(object sender, RoutedEventArgs e)
        {
            EditStandardPopup.IsOpen = false;

        }
        private void CloseAddpopup(object sender, RoutedEventArgs e)
        {
            StandardPopup.IsOpen = false;

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (DeletePopup.IsOpen) { DeletePopup.IsOpen = false; }
        }
        private void ShowPopupOffsetClickedEdit(object sender, RoutedEventArgs e)
        {
            if (!EditStandardPopup.IsOpen) { EditStandardPopup.IsOpen = true; }
            EditStandardPopup.IsOpen = true;
            EditTaskBox.Text = TaskList.SelectedValue.ToString();
            t1.task = EditTaskBox.Text;
            //EnterButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Visible;
            getDataDB();
        }

        //private void ShowPopupOffsetClickedView(object sender, RoutedEventArgs e)
        //{
        //    if (!ViewPopup.IsOpen) { ViewPopup.IsOpen = true; }
        //}
        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temporaryDate = "";
            ViewPopup.IsOpen = true;
            Details.Text = TaskList.SelectedValue.ToString();
            TaskBox.Text = Details.Text;
            t1.task = Details.Text;
            getDataDB();

            if (DeadlineDate.Date.ToString().Contains(" "))
            {
                //System.Diagnostics.Debug.WriteLine(DeadlineDate.Date.ToString());
                int indexOfDate = DeadlineDate.Date.ToString().IndexOf(" ");
                System.Diagnostics.Debug.WriteLine(indexOfDate);
                temporaryDate = DeadlineDate.Date.ToString().Substring(0, indexOfDate);
            }
            Date.Text = temporaryDate;
            System.Diagnostics.Debug.WriteLine(temporaryDate);
            Time.Text = DeadlineTime.Time.ToString();

        }

        private async void SetTaskUsingVoice(object sender, RoutedEventArgs e)
        {
            SetTaskUsingVoiceBtn.IsEnabled = false;
            var speechRecognizer = new SpeechRecognizer();
            await speechRecognizer.CompileConstraintsAsync();

            SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();
            var messageDialog = new MessageDialog(speechRecognitionResult.Text,"You said:");
            await messageDialog.ShowAsync();
            SetTaskUsingVoiceBtn.IsEnabled = true;
            taskQuery = speechRecognitionResult.Text;
            voiceQueryRecognize();
            sendVoiceQueryData();

        }
        private async void WelcomeVoiceTaskPage()
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("If you wish to add task using voice, Click Ask Costia. Or you can add task manually.");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private void voiceQueryRecognize()
        {
            //string taskQuery = "Remind me to buy tomatoes on 15th December 9 pm";
            //string taskQuery = "Hey Costia remind me to wake me at 12 pm in the today";

            //WE'VE  taskQuery FROM SetTaskUsingVoice FUNCTION.
            string[] days = { "morning", "evening", "night", "today", "yesterday", "tomorrow" };
            string[] months = {"january", "february", "march", "april", "may", "june", "july",
            "august", "september", "october", "november", "december"};
            int time = 0;
            //CONVERT IT TO LOWER CASE STRING FIRST OF ALL
            taskQuery = taskQuery.ToLower();
            //---------------------TASK ALGORITHM----------------------------------
            //In case of Remind me to , Hey Hello etc
            if (taskQuery.StartsWith("remind"))
            {
                taskQuery = taskQuery.Remove(0, 13);
            }
            else if (taskQuery.StartsWith("hey") || taskQuery.StartsWith("hello"))
            {
                taskQuery = taskQuery.Remove(0, 11);
                if (taskQuery.StartsWith("remind"))
                {
                    taskQuery = taskQuery.Remove(0, 13);
                }
            }
            //POSITION OF ACTION

            if (taskQuery.Contains(" at "))
            {
                int positionOfat = taskQuery.IndexOf("at");
                string taskQuerySubString = taskQuery.Substring(0, positionOfat);
                taskQuerySubString = char.ToUpper(taskQuerySubString[0]) + taskQuerySubString.Substring(1);
                //Console.WriteLine($"Task: {taskQuerySubString}");
                t1.task = taskQuerySubString;
            }
            else if (taskQuery.Contains(" on "))
            {
                int positionOfon = taskQuery.IndexOf("on");
                string taskQuerySubString = taskQuery.Substring(0, positionOfon);
                taskQuerySubString = char.ToUpper(taskQuerySubString[0]) + taskQuerySubString.Substring(1);
                //Console.WriteLine($"Task: {taskQuerySubString}");
                t1.task = taskQuerySubString;
            }

            //---------------------TIME ALGORITHM----------------------------------
            string timeQuery;
            string remainingQuery = "";
            int monthNum = 0;
            string date = "";
            if (taskQuery.Contains(" at "))
            {
                int positionOfat = taskQuery.IndexOf("at");
                timeQuery = taskQuery.Substring(positionOfat + 3);
                remainingQuery = timeQuery; // will be later use in date
                if (timeQuery.Contains(" pm"))
                {
                    if (timeQuery.Contains(":"))
                    {
                        timeQuery = timeQuery.Substring(0, 5);
                        if (timeQuery.Contains(" "))
                        {
                            timeQuery = timeQuery.Substring(0, 4);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                            timeQuery = timeQuery.Remove(0, 1);
                        }
                        else
                        {

                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                            timeQuery = timeQuery.Remove(0, 2);
                        }
                        if (time != 12)
                        {
                            time = time + 12;
                        }
                        timeQuery = $"{time.ToString()}{timeQuery}:00";
                    }
                    else
                    {
                        timeQuery = timeQuery.Substring(0, 2);
                        if (timeQuery.Contains(" "))
                        {
                            timeQuery = timeQuery.Substring(0, 1);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                        }
                        else
                        {
                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                        }
                        if (time != 12)
                        {
                            time = time + 12;
                        }
                        timeQuery = $"{time.ToString()}:00:00";
                    }
                    //Console.WriteLine($"Time:{timeQuery}");
                    t1.deadlineTime = timeQuery;
                }
                else // am when using at 
                {
                    if (timeQuery.Contains(":"))
                    {
                        timeQuery = timeQuery.Substring(0, 5);
                        if (timeQuery.Contains(" "))
                        {
                            timeQuery = timeQuery.Substring(0, 4);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                            timeQuery = timeQuery.Remove(0, 1);
                        }
                        else
                        {

                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                            timeQuery = timeQuery.Remove(0, 2);
                        }
                        if (time == 12)
                        {
                            time = 0;
                            timeQuery = $"{time.ToString()}0{timeQuery}:00";
                        }
                        else
                        {
                            timeQuery = $"{time.ToString()}{timeQuery}:00";
                        }
                    }
                    else
                    {
                        timeQuery = timeQuery.Substring(0, 2);
                        if (timeQuery.Contains(" "))
                        {
                            timeQuery = timeQuery.Substring(0, 1);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                            timeQuery = $"{time.ToString()}:00:00";
                        }
                        else
                        {
                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                            if (time == 12)
                            {
                                time = 0;
                                timeQuery = $"{time.ToString()}0:00:00";
                            }
                            else
                            {
                                timeQuery = $"{time.ToString()}:00:00";
                            }
                        }

                    }
                    //Console.WriteLine($"Time:{timeQuery}");
                    t1.deadlineTime = timeQuery;
                }
            }
            //--------------------DATE-TIME ALGO FOR ON STATEMENTS------------------
            else if (taskQuery.Contains(" on "))
            {
                int positionOfon = taskQuery.IndexOf("on");
                timeQuery = taskQuery.Substring(positionOfon + 3);
                //Console.WriteLine(timeQuery);
                for (int i = 0; i < 12; i++)
                {
                    if (timeQuery.Contains(months[i]))
                    {
                        monthNum = i + 1;
                        break;
                    }
                }
                if (timeQuery.Contains("th"))
                {
                    if (timeQuery.IndexOf("th") == 2)
                    {
                        date = timeQuery.Substring(0, 2);

                    }
                    else if (timeQuery.IndexOf("th") == 1)
                    {
                        date = timeQuery.Substring(0, 1);
                    }
                }
                else
                {
                    if (timeQuery.IndexOf(" ") == 1)
                    {
                        date = timeQuery.Substring(0, 1);
                    }
                    else if (timeQuery.IndexOf(" ") == 2)
                    {
                        date = timeQuery.Substring(0, 2);
                    }
                }
                date = $"{monthNum.ToString()}/{date}/2020";
                //Console.WriteLine($"Date:{date}");
                t1.deadlineDate = date;

                //---------TIME-----------
                if (timeQuery.Contains(" pm")) // pm with the use of on
                {
                    if (!timeQuery.Contains(":")) // simple time like 11 PM
                    {
                        timeQuery = timeQuery.Substring(timeQuery.IndexOf(" pm") - 2, 2);
                        if (timeQuery.StartsWith(" "))
                        {
                            timeQuery = timeQuery.Substring(1, 1);
                        }
                        else
                        {
                            timeQuery = timeQuery.Substring(0, 2);
                        }
                        time = Int32.Parse(timeQuery);
                        if (time != 12)
                        {
                            time = time + 12;
                        }
                        timeQuery = time.ToString();
                        timeQuery = $"{timeQuery}:00:00";
                        //Console.WriteLine(timeQuery);
                        t1.deadlineTime = timeQuery;
                    }
                    else
                    {
                        timeQuery = timeQuery.Substring(timeQuery.IndexOf(" pm") - 5, 5);

                        if (timeQuery.StartsWith(" "))
                        {
                            timeQuery = timeQuery.Substring(1);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                            if (time != 12)
                            {
                                time = time + 12;
                            }
                            timeQuery = timeQuery.Remove(0, 1);
                        }
                        else
                        {
                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                            if (time != 12)
                            {
                                time = time + 12;
                            }
                            timeQuery = timeQuery.Remove(0, 2);
                        }
                        timeQuery = $"{time.ToString()}{timeQuery}:00";
                        //Console.WriteLine(timeQuery);
                        t1.deadlineTime = timeQuery;
                    }
                }
                else if (timeQuery.Contains(" am"))
                {
                    if (!timeQuery.Contains(":"))
                    {
                        timeQuery = timeQuery.Substring(timeQuery.IndexOf(" am") - 2, 2);
                        if (timeQuery.StartsWith(" "))
                        {
                            timeQuery = timeQuery.Substring(1, 1);
                            timeQuery = $"{timeQuery}:00:00";
                        }
                        else
                        {
                            timeQuery = timeQuery.Substring(0, 2);
                            if (timeQuery == "12")
                            {
                                timeQuery = $"00:00:00";
                            }
                            else
                            {
                                timeQuery = $"{timeQuery}:00:00";
                            }
                        }
                        //Console.WriteLine(timeQuery);
                        t1.deadlineTime = timeQuery;
                    }
                    else
                    {
                        timeQuery = timeQuery.Substring(timeQuery.IndexOf(" am") - 5, 5);
                        if (timeQuery.StartsWith(" "))
                        {
                            timeQuery = timeQuery.Substring(1);
                            char[] myNameChars = timeQuery.ToCharArray();
                            time = Int32.Parse(myNameChars[0].ToString());
                            timeQuery = timeQuery.Remove(0, 1);
                        }
                        else
                        {
                            char[] myNameChars = timeQuery.ToCharArray();
                            string myNameCharsString = myNameChars[0].ToString() + myNameChars[1].ToString();
                            time = Int32.Parse(myNameCharsString);
                            if (time == 12)
                            {
                                time = 0;
                            }
                            timeQuery = timeQuery.Remove(0, 2);
                        }
                        if (time == 0)
                        {
                            timeQuery = $"{time.ToString()}0{timeQuery}:00";
                        }
                        else
                        {
                            timeQuery = $"{time.ToString()}{timeQuery}:00";
                        }
                        //Console.WriteLine($"Time:{timeQuery}");
                        t1.deadlineTime = timeQuery;
                    }
                }
            }
            //------------------------------------DATE Algorithm-------------------------
            foreach (string day in days)
            {
                if (remainingQuery.Contains(day))
                {
                    if (day != "yesterday" && day != "today")
                    {
                        DateTime now = DateTime.Now;
                        string dateQuery = now.AddDays(1).ToShortDateString();
                        //Console.WriteLine(dateQuery);
                        t1.deadlineDate = dateQuery;
                    }
                    else if (day == "today")
                    {
                        DateTime now = DateTime.Now;
                        string dateQuery = now.ToShortDateString();
                        //Console.WriteLine(dateQuery);
                        t1.deadlineDate = dateQuery;
                    }
                }

                else
                {
                    DateTime now = DateTime.Now;
                    string dateQuery = now.ToShortDateString();
                    //Console.WriteLine(dateQuery);
                    t1.deadlineDate = dateQuery;
                }
            }
        }
        private void sendVoiceQueryData()
        {
            t1.category = "Household";
            System.Diagnostics.Debug.WriteLine(t1.task);
            System.Diagnostics.Debug.WriteLine(t1.deadlineTime);
            System.Diagnostics.Debug.WriteLine(t1.deadlineDate);
            if (t1.task == null || t1.deadlineTime == null || t1.deadlineDate == null)
            {
                dialogBoxIfEmpty();
                return;
            }
            else
            {
                DataAccess.AddData(t1.task, t1.deadlineDate, t1.deadlineTime, t1.category);
                taskItems.Add(t1);
                generatingList();
            }
        }

        private async void dialogBoxIfEmpty()
        {
            var dialog = new MessageDialog(
                "Looks like your task is not heard correctly. Make sure to " +
                "specify date and time along with the task. (E.g Remind me to " +
                "do home work at 8:30 PM today or Read book on 4th August 9 AM)"
                );

            dialog.Commands.Add(new UICommand("OK") { Id = 0 });
            dialog.DefaultCommandIndex = 0;

            var result = await dialog.ShowAsync();
        }

        private void notificationAlert()
        {
            DateTime rightNow = DateTime.Now;
            string rightNowStr = rightNow.ToString("M/dd/yyyy HH:mm:ss");
            List<String> notificationAlert = DataAccess.notificationGetDateTimeList();
            foreach(string notification in notificationAlert)
            {
                string id = notification.Substring(0, notification.IndexOf(": "));
                string notification1 = notification.Remove(0,notification.IndexOf(": ")+2);
                if (rightNowStr == notification1)
                {
                    notificationData.notificationId = Convert.ToInt32(id);
                    System.Diagnostics.Debug.WriteLine(notification1);
                }
            }

        }
    }
}
        
    
