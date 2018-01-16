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
    public sealed partial class AddStudent : Page
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
        public AddStudent()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
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

            try
            {
                await BluetoothConnection.SendCommand("+");
                FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            }
            catch (Exception)
            {
                FingerStatusText.Text = "An error occured!";
            }
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            try
            {
                await BluetoothConnection.SendCommand(",");
                //gloablvalue.RecivedText = "";
                //gloablvalue.Status = "";
            }
            catch (Exception)
            {
                FingerStatusText.Text = "An error occured!";
            }
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
            //Frame.Navigate(typeof(AddStudent));
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
            Frame.Navigate(typeof(NewLec));
        }
        private void AllStudentsBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllStudents));
        }


        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            DatabaseManagement.InsertAllStudentsData(tbxAcadimicId.Text, tbxName.Text, newfpid.Text);
            var dlg = new MessageDialog(tbxName.Text + " added.");
            await dlg.ShowAsync();
            tbxAcadimicId.Text = "";
            tbxName.Text = "";
            FINGER.IsTapEnabled = true;
            btnAdd.IsEnabled = false;
            //try
            //{
            await BluetoothConnection.SendCommand("+");
            FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            BtN_Menu.IsTapEnabled = true;
            //}
            //catch (Exception)
            //{
            //    var dlg2 = new MessageDialog("error to run device again!");
            //    await dlg.ShowAsync();
            //    BtN_Menu.IsTapEnabled = true;
            //}
            //}
            //catch (Exception)
            //{
            //    var dlg = new MessageDialog("error to add in database!");
            //    await dlg.ShowAsync();
            //}
        }

        private async void HideFINGER_Completed(object sender, object e)
        {
            FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            if (gloablvalue.checksend(gloablvalue.RecivedText) == "Stored!")
            {
                FingerStatusText.Text = "Id = " + newfpid.Text + " Stored!";
                btnAdd.IsEnabled = true;
                gloablvalue.RecivedText = "";
                HideFINGER.Stop();
                ShowFINGER.Stop();
            }
            else if (gloablvalue.checksend(gloablvalue.RecivedText) == "Fingerprints did not match")
            {
                FingerStatusText.Text = "Fingerprints did not match"
                                            + "\r\n" + "Click on Take fingerprint to try again";
                HideFINGER.Stop();
                ShowFINGER.Stop();
                await BluetoothConnection.SendCommand("+");
                FINGER.IsTapEnabled = true;
                BtN_Menu.IsTapEnabled = true;
            }
            else
            {
                FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
                //FingerStatusText.Text = "Error at receiving, please click again";
                //HideFINGER.Stop();
                //ShowFINGER.Stop();
                //await BluetoothConnection.SendCommand("+");
                //FINGER.IsTapEnabled = true;
                //BtN_Menu.IsTapEnabled = true;
                ShowFINGER.Begin();
            }
        }
        private async void ShowFINGER_Completed(object sender, object e)
        {
            FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
            if (gloablvalue.checksend(gloablvalue.RecivedText) == "Stored!")
            {
                FingerStatusText.Text = "Id = " + newfpid.Text + " Stored!";
                btnAdd.IsEnabled = true;
                gloablvalue.RecivedText = "";
                HideFINGER.Stop();
                ShowFINGER.Stop();
            }
            else if (gloablvalue.checksend(gloablvalue.RecivedText) == "Fingerprints did not match")
            {
                FingerStatusText.Text = "Fingerprints did not match"
                                            + "\r\n" + "Click on Take fingerprint to try again";
                HideFINGER.Stop();
                ShowFINGER.Stop();
                await BluetoothConnection.SendCommand("+");
                FINGER.IsTapEnabled = true;
                BtN_Menu.IsTapEnabled = true;
            }
            else
            {
                FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
                //FingerStatusText.Text = "Error at receiving, please click again";
                //HideFINGER.Stop();
                //ShowFINGER.Stop();
                //await BluetoothConnection.SendCommand("+");
                //FINGER.IsTapEnabled = true;
                //BtN_Menu.IsTapEnabled = true;
                HideFINGER.Begin();
            }
        }

        private async void FINGER_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Validations.CheckTextBox(tbxName, tbxAcadimicId).Equals(true))
            {
                var dialog = new MessageDialog("Check Valuing fields");
                await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    await DatabaseManagement.CountAllStudentsData(newfpid);
                    try
                    {
                        await BluetoothConnection.SendCommand(newfpid.Text);
                        FingerStatusText.Text = gloablvalue.checksend(gloablvalue.RecivedText);
                        FINGER.IsTapEnabled = false;
                        HideFINGER.Begin();
                        BtN_Menu.IsTapEnabled = false;
                    }
                    catch (Exception)
                    {
                        FingerStatusText.Text = "error in sending fingerprint id !";
                    }
                }
                catch (Exception)
                {
                    FingerStatusText.Text = "error in count student data!";
                }
            }
        }
    }
}
