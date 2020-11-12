using COMP7216Prototype.Data;
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
                    Email.Text = person.Email;
                    await DisplayAlert("Success", "Person Name: " + person.Email, "OK");
                }
                else if (person == null)
                {

                    await DisplayAlert("Wrong", "Incorrect Email, Please try again ", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please fill in all the planks", "OK");
            }
            //Checking Password
            if (!string.IsNullOrEmpty(Password.Text))
            {
                var person = await App.SQLiteDb.GetPasswordAsync(Password.Text);
                if (person != null)
                {
                    Password.Text = person.Password;
                    await DisplayAlert("Success", "Password: " + person.Password, "OK");
                }
                else if (person == null)
                {

                    await DisplayAlert("Wrong", "Incorrect Password, Please try again ", "OK");
                }
            }
            //if (emailCorrect && passw)
            //{

            //}
            else
            {
                await DisplayAlert("Required", "Please fill in all the planks", "OK");
            }
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}