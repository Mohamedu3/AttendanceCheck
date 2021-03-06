﻿using AttendanceCheck.Common;
using AttendanceCheck.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceCheck.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Edit : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Edit()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            if (gloablvalue.PageType == "1")
            {
                tbxFirstname.Text = gloablvalue.Acc_fname;
                tbxLastname.Text = gloablvalue.Acc_lname;
                pbxPassword.Password = gloablvalue.Acc_password;
            }
            else if (gloablvalue.PageType == "2")
            {
                pbxPassword.Visibility = Visibility.Collapsed;
                Labelpassword.Visibility = Visibility.Collapsed;

                Labelfname.Text = "Acadimic Id: ";
                Labellname.Text = "Name: ";

                tbxFirstname.Text = gloablvalue.Student_acadimicid;
                tbxLastname.Text = gloablvalue.Student_name;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (gloablvalue.PageType == "1")
            {
                Frame.Navigate(typeof(AllAccounts));
            }
            else if (gloablvalue.PageType == "2")
            {
                Frame.Navigate(typeof(AllStudents));
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (gloablvalue.PageType == "1")
            {
                if (Validations.CheckTextBox(tbxFirstname, tbxLastname).Equals(true))
                {
                    var dialog = new MessageDialog("Check Valuing fields");
                    await dialog.ShowAsync();
                }
                else if (pbxPassword.Password.Count() < 8)
                {
                    var dialog = new MessageDialog("Password less than 8 characters");
                    await dialog.ShowAsync();
                }
                else
                {
                    //try
                    //{
                    DatabaseManagement.UpdateAllAccountData(gloablvalue.Acc_username, tbxFirstname.Text, tbxLastname.Text, gloablvalue.Acc_password, pbxPassword.Password);
                    var dialog = new MessageDialog("Updated.");
                    await dialog.ShowAsync();
                    //}
                    //catch (Exception)
                    //{
                    //var dialog0 = new MessageDialog("An error occured.");
                    //}
                }
                Frame.Navigate(typeof(AllAccounts));
            }
            else if (gloablvalue.PageType == "2")
            {
                if (Validations.CheckTextBox(tbxFirstname, tbxLastname).Equals(true))
                {
                    var dialog = new MessageDialog("Check Valuing fields");
                    await dialog.ShowAsync();

                }
                else
                {
                    //try
                    //{
                    DatabaseManagement.UpdateStudentData(tbxFirstname.Text, tbxLastname.Text);
                    var dialog = new MessageDialog("Updated.");
                    await dialog.ShowAsync();
                    //}
                    //catch (Exception)
                    //{
                    //    var dialog = new MessageDialog("An error occured.");
                    //    await dialog.ShowAsync();
                    //}
                }
                Frame.Navigate(typeof(AllStudents));
            }
        }
    }
}

