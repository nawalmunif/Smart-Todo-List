using System;
using System.Collections.Generic;
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
using Windows.UI.Popups;
using DataAccessLibrary;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartToDoList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmTone : Page
    {
        public AlarmTone()
        {
            this.InitializeComponent();
        }
        //int snooze;
        //string audioSrc;

        //private void AlarmTimePickerHours(object sender, TimePickerValueChangedEventArgs e)
        //{
        //    Int32 hour = timepicker.Time.Hours;
        //}
        //private void AlarmTimePickerMinutes(object sender, TimePickerValueChangedEventArgs e)
        //{
        //    Int32 min = timepicker.Time.Minutes;
        //}
        //private void AlarmTimePickerSeconds(object sender, TimePickerValueChangedEventArgs e)
        //{
        //    Int32 sec = timepicker.Time.Seconds;
        //}
        //private void AlarmDatePickerYear(DatePicker sender, DatePickerValueChangedEventArgs e)
        //{
        //    Int32 year = datepicker.Date.Year;
        //}
        //private void AlarmDatePickerMonth(DatePicker sender, DatePickerValueChangedEventArgs e)
        //{
        //    Int32 month = datepicker.Date.Month;
        //}
        //public void AlarmDatePickerDay(DatePicker sender, DatePickerValueChangedEventArgs e)
        //{
        //    Int32 days = datepicker.Date.Day;
        //}
    }
}

//        DateTimeOffset myDate1 = new DateTime(a);
        
//        DateTime myDate2 = DateTime.Now;
//        TimeSpan myDateResult = new TimeSpan();
//        myDateResult = myDate1 - myDate2;
//    if (myDate2 > myDate1)
//    {
//        var x = new MessageDialog("Invalid date or time");
//        await x.ShowAsync();
//    }
//    else
//    {
//        string title = "Alarm!";
//    string message = alm_msg.Text;
//    string imgURL = "ms-appx:///Assets/Capture.PNG";

//    string toastXmlString =
//     "<toast><visual version='1'>
//     < binding template='toastImageAndText02'><text id = '1' > " 
//    + title + "</text><text id='2'>"
//         + message + "</text><image id='1' src='" +
//         imgURL + "'/></binding></visual>\n" +
//       "<commands scenario=\"alarm\">\n" +
//             "<command id=\"snooze\"/>\n" +
//             "<command id=\"dismiss\"/>\n" +
//         "</commands>\n" +

//               "<audio src='ms-winsoundevent:Notification." + audioSrc + "'/>" +
//         "</toast>";

//    Windows.Data.Xml.Dom.XmlDocument toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
//    toastDOM.LoadXml(toastXmlString);
//        var toastNotifier1 = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();

//    double x1 = myDateResult.TotalSeconds;
//    int customSnoozeSeconds = snooze * 60;

//    TimeSpan snoozeInterval = TimeSpan.FromSeconds(customSnoozeSeconds);

//    var customAlarmScheduledToast = new Windows.UI.Notifications.ScheduledToastNotification
//    (toastDOM, DateTime.Now.AddSeconds(x1), snoozeInterval, 0);

//    toastNotifier1.AddToSchedule(customAlarmScheduledToast);
//        var x = new MessageDialog("Alarm Set!");
//    await x.ShowAsync();
//}
//    }
//}
