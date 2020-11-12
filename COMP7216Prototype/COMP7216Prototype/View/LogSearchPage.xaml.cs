using COMP7216Prototype.Controller;
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
        public LogSearchPage()
        {
            InitializeComponent();
            searchController = new Search();
            BindingContext = searchController;
        }

        private async void SearchSharesClicked(object sender, EventArgs e)
        {
            var results = searchController.SearchShares(entryShareSearch.Text);
            await Navigation.PushAsync(new ResultsReportPage(results));
        }

        private async void SearchRequestsClicked(object sender, EventArgs e)
        {
            var results = searchController.SearchShares(entryRequestSearch.Text);
            await Navigation.PushAsync(new ResultsReportPage(results));
        }

        private async void ViewAllSharesClicked(object sender, EventArgs e)
        {
            var results = searchController.ViewShares();
            await Navigation.PushAsync(new ResultsReportPage(results));
        }

        private async void ViewAllRequestsClicked(object sender, EventArgs e)
        {
            var results = searchController.ViewRequests();
            await Navigation.PushAsync(new ResultsReportPage(results));
        }
    }
}