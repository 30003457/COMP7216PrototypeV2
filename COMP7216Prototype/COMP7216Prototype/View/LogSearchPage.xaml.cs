using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Graphics.Display;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogSearchPage : ContentPage
    {
        
        Search searchController;
        public Customers loggedInUser { get; set; }
        public LogSearchPage(Customers _loggedInUser)
        {
            InitializeComponent();
            loggedInUser = _loggedInUser;
            searchController = new Search();
            BindingContext = searchController;
        }

        private async void SearchSharesClicked(object sender, EventArgs e)
        {
            var results = searchController.SearchShares(entryShareSearch.Text, loggedInUser);
            await Navigation.PushAsync(new ResultsReportPage(results, loggedInUser));
        }

        private async void SearchRequestsClicked(object sender, EventArgs e)
        {
            var results = searchController.SearchRequests(entryRequestSearch.Text, loggedInUser);
            await Navigation.PushAsync(new ResultsReportPage(results, loggedInUser));
        }

        private async void ViewAllSharesClicked(object sender, EventArgs e)
        {
            var results = searchController.ViewShares(loggedInUser);
            await Navigation.PushAsync(new ResultsReportPage(results, loggedInUser));
        }

        private async void ViewAllRequestsClicked(object sender, EventArgs e)
        {
            var results = searchController.ViewRequests(loggedInUser);
            await Navigation.PushAsync(new ResultsReportPage(results, loggedInUser));
        }
    }
}