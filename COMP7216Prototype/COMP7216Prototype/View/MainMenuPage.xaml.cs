using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        DataAccessLayer dal = new DataAccessLayer();
        public string minutes { get; set; }
        public string data { get; set; }
        public string credits { get; set; }
        public string texts { get; set; }
        public Customers loggedInUser { get; set; }
        public bool newReqsVisible { get; set; }
        public MainMenuPage(Customers _loggedInUser)
        {
            InitializeComponent();
            loggedInUser = _loggedInUser;
            GetUserCredits();
            CheckRequests();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUserCredits();
            CheckRequests();
            BindingContext = null;
            BindingContext = this;
        }

        private void CheckRequests()
        {
            var requests = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE shareUserId={loggedInUser.customerId} AND requestAccepted=False");
            if (requests.Count > 0) newReqsVisible = true;
            else newReqsVisible = false;
        }

        private void GetUserCredits()
        {
            var userCredits = dal.db.Query<Credits>($"SELECT * FROM Credits WHERE customerId={loggedInUser.customerId}");
            foreach (var item in userCredits)
            {
                if (item.creditTypeId == 1) credits = item.amount.ToString();
                if (item.creditTypeId == 2) minutes = item.amount.ToString();
                if (item.creditTypeId == 3) data = item.amount.ToString();
                if (item.creditTypeId == 4) texts = item.amount.ToString();
            }
        }
        private async void BtnRequest_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(loggedInUser));
        }
        private async void BtnShare_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreditShareForm(loggedInUser));
        }
        private async void BtnHistory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogSearchPage(loggedInUser));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewRequests(loggedInUser));
        }
    }

}