using AttendanceCheck.Common;
using AttendanceCheck.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Attendance : Page
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

        public Attendance()
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
            Title.Text = gloablvalue.CurrentLecName + "/Attendance";
            await DatabaseManagement.LoadAllStudentsInLecData(MainLongListSelector);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private async void SearchBox_SuggestionsRequested(SearchBox sender, SearchBoxSuggestionsRequestedEventArgs args)
        {
            gloablvalue.SearchStudentId = SearchBox.QueryText;
            await DatabaseManagement.SearchStudentsInLecData(result);
            if (SearchBox.QueryText == "")
            {
                result.Text = "";
            }
            else if (result.Text == SearchBox.QueryText)
            {
                result.Text = "";
                await DatabaseManagement.LoadSpacificStudentInLecData(MainLongListSelector);
            }
            else
            {
                await DatabaseManagement.LoadAllStudentsInLecData(MainLongListSelector);
            }
        }

        private void MainLongListSelector_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewLec));
        }
        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
