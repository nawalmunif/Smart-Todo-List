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
using DataAccessLibrary;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartToDoList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Categories : Page
    {
        public Categories()
        {
            this.InitializeComponent();
            generatingMeetingList();
            generatingBusinessList();
            generatingHouseholdList();
            generatingProjectList();
        }
        public void MeetingList(object sender, SelectionChangedEventArgs e)
        {
            generatingMeetingList();
        }
        public void generatingMeetingList()
        {

            MeetingsTask.ItemsSource = DataAccess.GetMeetingTask();


        }
        public void BusinessList(object sender, SelectionChangedEventArgs e)
        {
            generatingBusinessList();
        }
        public void generatingBusinessList()
        {

            BusinessTask.ItemsSource = DataAccess.GetBusinessTask();


        }
        public void HouseholdList(object sender, SelectionChangedEventArgs e)
        {
            generatingHouseholdList();
        }
        public void generatingHouseholdList()
        {

            HouseholdTask.ItemsSource = DataAccess.GetHouseholdTask();


        }
        public void ProjectList(object sender, SelectionChangedEventArgs e)
        {
            generatingProjectList();
        }
        public void generatingProjectList()
        {

            ProjectTask.ItemsSource = DataAccess.GetProjectTask();


        }
        private void Home(object sender, RoutedEventArgs e)
        {


            
            this.Frame.Navigate(typeof(TaskPage));
        }

        private void HeaderYourTasks_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
    }

