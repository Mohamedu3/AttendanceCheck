using AttendanceCheck.Common;
using AttendanceCheck.Tables;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceCheck.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllLec : Page
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

        public AllLec()
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

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

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
            await DatabaseManagement.LoadAllLecturesData(MainLongListSelector);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        private void MainLongListSelector_ItemClick(object sender, ItemClickEventArgs e)
        {
            gloablvalue.CurrentLecId = ((Lectures)e.ClickedItem).Id.ToString();
            gloablvalue.CurrentLecName = ((Lectures)e.ClickedItem).LectureName;
            gloablvalue.CurrentLecDatetime = ((Lectures)e.ClickedItem).Datetime;
            gloablvalue.CurrentLecDuration = ((Lectures)e.ClickedItem).Duration;
            gloablvalue.CurrentLecRoom = ((Lectures)e.ClickedItem).Room;

            Frame.Navigate(typeof(ViewLec));
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
            //Frame.Navigate(typeof(AllLec));
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




    }
}
