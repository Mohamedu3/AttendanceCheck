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
    public sealed partial class Settings : Page
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

        public Settings()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
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
            //Frame.Navigate(typeof(Settings));
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


        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (gloablvalue.Currentpassword == tbxOldPassword.Password)
            {
                if (pbxNewPassword.Password != pbxNewRePassword.Password)
                {
                    var dialog = new MessageDialog("new password didn't matched !");
                    await dialog.ShowAsync();
                }
                else
                {
                    //try
                    //{
                    DatabaseManagement.UpdateAccountData(gloablvalue.Currentusername, tbxOldPassword.Password, pbxNewPassword.Password);
                    var dialog = new MessageDialog("Youry password has been changed successfully.");
                    await dialog.ShowAsync();
                    btnSave.IsEnabled = false;
                    //}
                    //catch (Exception)
                    //{
                    //    var dialog = new MessageDialog("an error occurred.");
                    //    await dialog.ShowAsync();
                    //}
                }
            }
            else if (gloablvalue.Currentpassword != tbxOldPassword.Password)
            {
                var dialog = new MessageDialog("wrong old password !");
                await dialog.ShowAsync();
            }
        }

        private async void DeleteBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Validations.MessageConfirmDeleteoUpdatePerson("Are you sure to delete your account?");

            if (Validations.result.Equals(true))
            {
                DatabaseManagement.DeleteAccount(gloablvalue.Currentusername);
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}

