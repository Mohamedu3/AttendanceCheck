using AttendanceCheck.Common;
using AttendanceCheck.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
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
    public sealed partial class Home : Page
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

        public Home()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
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

            switch (gloablvalue.DeviceState)
            {
                case "-2":
                    Devicestate_txt.Text = "An error occurred, try again.";
                    break;
                case "-1":
                    Devicestate_txt.Text = "Connect With Fingerprint Device !";
                    break;
                case "0":
                    Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Dissconnected.";
                    break;
                case "1":
                    Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Connected :)";
                    ConnectBTN.Content = "connect";
                    ConnectBTN.Tag = "Dissconnect";
                    ConnectBTN.IsEnabled = true;
                    break;
                case "2":
                    Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Bluetooth device not found, try again.";
                    break;
                case "3":
                    Devicestate_txt.Text = "Not found any device paired, try again.";
                    break;
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            //await BluetoothConnection.SendCommand(",");
            //gloablvalue.RecivedText = "";
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
            //Frame.Navigate(typeof(Home));
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
            //BluetoothConnection.CloseDevice();
            //gloablvalue.DeviceName = "";
            //gloablvalue.DeviceState = "-1";
            //gloablvalue.VaildationUsername = "";
            //gloablvalue.VaildationLoginUsername = "";
            //gloablvalue.VaildationLoginPassword = "";
            //gloablvalue.CurrentId = "";
            //gloablvalue.Currentusername = "";
            //gloablvalue.Currentfname = "";
            //gloablvalue.Currentlname = "";
            //gloablvalue.Currentpassword = "";
            //gloablvalue.Currenttype = "";
            //gloablvalue.CurrentLecId = "";
            //gloablvalue.CurrentLecName = "";
            //gloablvalue.CurrentLecDatetime = "";
            //gloablvalue.CurrentLecDuration = "";
            //gloablvalue.CurrentLecRoom = "";
            //gloablvalue.NewLecId = "";
            //gloablvalue.SearchStudentId = "";
            //gloablvalue.SearchAccountUsername = "";
            //gloablvalue.RecivedText = "";
            //gloablvalue.Status = "";
            //gloablvalue.attendid = "";
            //gloablvalue.PageType = "";
            //gloablvalue.Acc_id = "";
            //gloablvalue.Acc_username = "";
            //gloablvalue.Acc_fname = "";
            //gloablvalue.Acc_lname = "";
            //gloablvalue.Acc_type = "";
            //gloablvalue.Acc_password = "";
            //gloablvalue.Student_acadimicid = "";
            //gloablvalue.Student_name = "";
            Frame.Navigate(typeof(MainPage));
        }
        private void NewLecBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewLec));
        }
        private void AllStudentsBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllStudents));
        }

        private void Hide_Completed(object sender, object e)
        {
            if (gloablvalue.DeviceState == "-2")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "An error occurred, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "-1")
            {
                Show.Begin();
            }
            else if (gloablvalue.DeviceState == "0")
            {
                BTImg.Opacity = 1;
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Dissconnected";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "1")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Connected";
                ConnectBTN.Content = "Dissconnect";
                ConnectBTN.Tag = "Dissconnect";
                ConnectBTN.IsEnabled = true;
                //await BluetoothConnection.SendCommand("*");
                newrec_txt.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            }
            else if (gloablvalue.DeviceState == "2")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Bluetooth device not found, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "3")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "Not found any device paired, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
        }
        private void Show_Completed(object sender, object e)
        {
            if (gloablvalue.DeviceState == "-2")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "An error occurred, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "-1")
            {
                Hide.Begin();
            }
            else if (gloablvalue.DeviceState == "0")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Dissconnected";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "1")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Connected";
                ConnectBTN.Content = "Dissconnect";
                ConnectBTN.Tag = "Dissconnect";
                ConnectBTN.IsEnabled = true;
                //await BluetoothConnection.SendCommand("*");
                newrec_txt.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            }
            else if (gloablvalue.DeviceState == "2")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Bluetooth device not found, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
            else if (gloablvalue.DeviceState == "3")
            {
                Show.Stop();
                Hide.Stop();
                Devicestate_txt.Text = "Not found any device paired, try again.";
                ConnectBTN.Content = "connect";
                ConnectBTN.Tag = "connect";
                ConnectBTN.IsEnabled = true;
                gloablvalue.DeviceState = "-1";
            }
        }
        private async void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            if (txtbxDeviceName.Text != "")
            {
                if (ConnectBTN.Tag.ToString() == "connect")
                {
                    gloablvalue.DeviceName = txtbxDeviceName.Text;
                    ConnectBTN.IsEnabled = false;
                    if (gloablvalue.DeviceState == "-1")//device not connect
                    {
                        BluetoothConnection.ConnectToDevice();
                        Devicestate_txt.Text = "Connecting...";
                        newrec_txt.Text = gloablvalue.checksend(gloablvalue.RecivedText);
                        Hide.Begin();
                    }
                }
                else if (ConnectBTN.Tag.ToString() == "Dissconnect")
                {
                    if (gloablvalue.DeviceState == "1")//device connected
                    {
                        ConnectBTN.IsEnabled = false;
                        try
                        {
                            BluetoothConnection.CloseDevice();
                            Devicestate_txt.Text = "' " + gloablvalue.DeviceName + " ' " + " Dissconnected";
                            ConnectBTN.Content = "connect";
                            ConnectBTN.Tag = "connect";
                            ConnectBTN.IsEnabled = true;
                            //gloablvalue.DeviceState = "-1";
                        }
                        catch (Exception)
                        {
                            // ...
                        }
                    }
                }
            }
            else
            {
                var dlg = new MessageDialog("please type a device name..");
                await dlg.ShowAsync();
            }
        }
    }
}

