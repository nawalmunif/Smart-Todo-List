﻿using System;
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
    public sealed partial class IncompleteTask : Page
    {
        public IncompleteTask()
        {
            this.InitializeComponent();
            generatingList();
        }
        public void InCompleteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            generatingList();
        }
        public void generatingList()
        {

            PendingTaskList.ItemsSource = DataAccess.GetInCompletedTask();


        }
        public void BackfromIncomplete(object sender, RoutedEventArgs e) 
        {
            this.Frame.Navigate(typeof(TaskPage));
        }
    }
    }
