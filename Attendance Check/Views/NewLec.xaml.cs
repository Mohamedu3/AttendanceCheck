using AttendanceCheck.Common;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceCheck.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewLec : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        String datetime;
        DispatcherTimer mytimer = new DispatcherTimer();
        int s = 0;
        int m = 0;
        int h = 0;

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public NewLec()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            mytimer.Interval = new TimeSpan(0, 0, 1);
            mytimer.Tick += mytimer_Tick;
        }

        private void mytimer_Tick(object sender, object e)
        {
            if (s == 59)
            {
                s = 0;
                m = ++m;
            }
            if (m == 59)
            {
                m = 0;
                h = ++h;
            }

            s = ++s;

            if (s < 10)
            {
                st.Text = "0" + (s).ToString();
            }
            else
            {
                st.Text = (s).ToString();
            }

            if (m < 10)
            {
                mt.Text = "0" + (m).ToString();
            }
            else
            {
                mt.Text = (m).ToString();
            }

            if (h < 10)
            {
                ht.Text = "0" + (h).ToString();
            }
            else
            {
                ht.Text = (h).ToString();
            }
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            if (gloablvalue.Currenttype != "Admin")
            {
                NewAccountBtn.Visibility = Visibility.Collapsed;
                AllAccountsBtn.Visibility = Visibility.Collapsed;
                AddStudentBtn.Visibility = Visibility.Collapsed;
                AllStudentsBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                NewLecBtn.Visibility = Visibility.Collapsed;
                AllLecBtn.Visibility = Visibility.Collapsed;
            }
            if (gloablvalue.DeviceState == "1")
            {
                FingerStatusBorder.Opacity = 0;
                FINGER.Visibility = Visibility.Collapsed;
                EndButton.Visibility = Visibility.Collapsed;
                FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            }
            else
            {
                FingerStatusText.Text = "Device not connected!";
                StartButton.IsEnabled = false;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void Btn_Menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BtN_Menu.Source = new BitmapImage(new Uri("ms-appx:///Assets/option1.png", UriKind.Absolute));
            menu.Visibility = Visibility.Visible;
            LeftPanel.Visibility = Visibility.Visible;
            brd.Visibility = Visibility.Visible;
            ShowMenu.Begin();
            Showbrd.Begin();
            HideMenuBtn.Begin();
            OpenRotateBtnMenu.Begin();
            BtN_Menu.Visibility = Visibility.Collapsed;
        }
        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BtN_Menu.Source = new BitmapImage(new Uri("ms-appx:///Assets/option2.png", UriKind.Absolute));
            menu.Visibility = Visibility.Collapsed;
            LeftPanel.Visibility = Visibility.Collapsed;
            brd.Visibility = Visibility.Collapsed;
            HideMenu.Begin();
            Hidebrd.Begin();
            HideMenuBtn.Begin();
            CloseRotateBtnMenu.Begin();
            BtN_Menu.Visibility = Visibility.Visible;
        }
        private void HomeBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));
        }
        private void NewAccountBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(register));
        }
        private void AllAccountsBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllAccounts));
        }
        private void AddStudentBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddStudent));
        }
        private void AllLecBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllLec));
        }
        private void settingsBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
        private void LogOutBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void NewLecBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Frame.Navigate(typeof(NewLec));
        }
        private void AllStudentsBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllStudents));
        }
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validations.CheckTextBox(txtbxLecName, txtbxRoom).Equals(true))
            {
                var dialog = new MessageDialog("Check Valuing fields");
                await dialog.ShowAsync();
            }
            else
            {
                //try
                //{
                await DatabaseManagement.CountAllLecturesData(lecid);
                gloablvalue.NewLecId = lecid.Text;
                FingerStatusBorder.Opacity = 0.75;
                mytimer.Start();
                Stopwatch.Visibility = Visibility.Visible;
                FINGER.Visibility = Visibility.Visible;
                DataGrid.Visibility = Visibility.Collapsed;
                BtN_Menu.Visibility = Visibility.Collapsed;
                DateTime TimeDate = DateTime.Now; //time and date
                datetime = TimeDate.ToString();
                Title.Text = txtbxLecName.Text;
                StartButton.IsEnabled = false;
                EndButton.Visibility = Visibility.Visible;
                //}
                //catch (Exception)
                //{
                //    var dialog = new MessageDialog("Error in count lectures numbers!");
                //    await dialog.ShowAsync();
                //}
            }
        }

        private async void HideFINGER_Completed(object sender, object e)
        {
            FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            gloablvalue.attendid = gloablvalue.checksend(gloablvalue.RecivedText);

            if (gloablvalue.attendid == "F")
            {
                FingerStatusText.Text = "Put your finger!";
            }
            else
            {
                FingerStatusText.Text = gloablvalue.attendid;
                await DatabaseManagement.SearchStudentFingerPrint(studentid, Acadimicid, studentname, fingerprintid); //get fingerprint was get from fingerprint and search on student id if found return it
                if (studentid.Text == "NULL")
                {
                    FingerStatusText.Text = "An error occured!";
                }
                else if (studentid.Text == "Not Found")
                {
                    FingerStatusText.Text = "Id = " + gloablvalue.attendid + " not stored!";
                }
                else
                {
                    await DatabaseManagement.CheckAttend(state);
                    if (state.Text == "Not Found")
                    {
                        DatabaseManagement.InsertAllStudentsInLecData(gloablvalue.NewLecId, studentid.Text, Acadimicid.Text, studentname.Text, fingerprintid.Text);
                        FingerStatusText.Text = "Done id = " + gloablvalue.attendid;
                    }
                    else if (state.Text == "Found")
                    {
                        FingerStatusText.Text = "Id = " + gloablvalue.attendid + " Attend Before";
                    }
                    else
                    {
                        FingerStatusText.Text = "An error occured!";
                    }
                }
                //try
                //{
                await BluetoothConnection.SendCommand("-");
                //}
                //catch (Exception)
                //{
                //    var dialo2g = new MessageDialog("error in start device, click again.");
                //    await dialo2g.ShowAsync();
                //}
            }
            ShowFINGER.Begin();
        }
        private async void ShowFINGER_Completed(object sender, object e)
        {
            FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            gloablvalue.attendid = gloablvalue.checksend(gloablvalue.RecivedText);

            if (gloablvalue.attendid == "F")
            {
                FingerStatusText.Text = "Put your finger!";
            }
            else
            {
                FingerStatusText.Text = gloablvalue.attendid;
                await DatabaseManagement.SearchStudentFingerPrint(studentid, Acadimicid, studentname, fingerprintid); //get fingerprint was get from fingerprint and search on student id if found return it
                if (studentid.Text == "NULL")
                {
                    FingerStatusText.Text = "An error occured!";
                }
                else if (studentid.Text == "Not Found")
                {
                    FingerStatusText.Text = "Id = " + gloablvalue.attendid + " not stored!";
                }
                else
                {
                    await DatabaseManagement.CheckAttend(state);
                    if (state.Text == "Not Found")
                    {
                        DatabaseManagement.InsertAllStudentsInLecData(gloablvalue.NewLecId, studentid.Text, Acadimicid.Text, studentname.Text, fingerprintid.Text);
                        FingerStatusText.Text = "Done id = " + gloablvalue.attendid;
                    }
                    else if (state.Text == "Found")
                    {
                        FingerStatusText.Text = "Id = " + gloablvalue.attendid + " Attend Before";
                    }
                    else
                    {
                        FingerStatusText.Text = "An error occured!";
                    }
                }
                //try
                //{
                await BluetoothConnection.SendCommand("-");
                //}
                //catch (Exception)
                //{
                //    var dialo2g = new MessageDialog("error in start device, click again.");
                //    await dialo2g.ShowAsync();
                //}
            }
            HideFINGER.Begin();
        }

        private async void EndButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            string duration = ht.Text + ":" + mt.Text + ":" + st.Text;
            DatabaseManagement.InsertLectureData(txtbxLecName.Text, gloablvalue.CurrentId, datetime, duration, txtbxRoom.Text);
            mytimer.Stop();
            HideFINGER.Stop();
            ShowFINGER.Stop();
            gloablvalue.NewLecId = "";
            //try
            //{
            await BluetoothConnection.SendCommand(",");
            Frame.Navigate(typeof(AllLec));
            //}
            //catch (Exception)
            //{
            //    var dialo2g = new MessageDialog("device not ending the fingerprint..!");
            //    await dialo2g.ShowAsync();
            //}
            //}
            //catch (Exception)
            //{
            //    var dialog = new MessageDialog("Error in insert lecture data !");
            //    await dialog.ShowAsync();
            //}
        }
        private async void FINGERIMG_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //try
            //{
            await BluetoothConnection.SendCommand("-");
            HideFINGER.Begin();
            FINGERIMG.IsTapEnabled = false;
            Title2.Text = "Taking attendance..";
            //}
            //catch (Exception)
            //{
            //    var dialo2g = new MessageDialog("error in start device, click again.");
            //    await dialo2g.ShowAsync();
            //}
        }
    }
}
