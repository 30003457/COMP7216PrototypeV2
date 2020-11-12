using COMP7216Prototype.Data;
using COMP7216Prototype.Model;
using SQLite;
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
    public partial class LoginPage : ContentPage
    {
        private bool emailCorrect;
        private bool passwordCorrect;
        //private SQLiteConnection database;
        public LoginPage()
        {
            InitializeComponent();

        }
        private async void BtnReset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Persons  
            var personList = await App.SQLiteDb.GetItemsAsync();
            if (personList != null)
            {
                lstPersons.ItemsSource = personList;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //Check Email
            if (!string.IsNullOrEmpty(Email.Text))
            {
                var person = await App.SQLiteDb.GetEmailAsync(Email.Text);
                if (person != null)
                {
                    Email.Text = person.email;
                    emailCorrect = true;
                }
                else if (person == null)
                {
                    emailCorrect = false;
                }
            }

            //Checking Password
            if (!string.IsNullOrEmpty(Password.Text))
            {
                var person = await App.SQLiteDb.GetPasswordAsync(Password.Text);
                if (person != null)
                {
                    Password.Text = person.password;
                    passwordCorrect = true;
                }
                else if (person == null)
                {
                    passwordCorrect = false;
                }
            }
            if (emailCorrect && passwordCorrect)
            {
                await DisplayAlert("Success", "Your are logged in: " + Email.Text, "OK");
            }
            else if (string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(Email.Text))
            {
                await DisplayAlert("Required", "Please fill in all the planks", "OK");
            }
            else if (!emailCorrect || !passwordCorrect)
            {
                await DisplayAlert("Wrong", "Incorrect Info, Please try again ", "OK");
            }

        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}