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
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
        }
        private async void BtnReset_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Email.Text))
            {
                await DisplayAlert("Sent!", "Reset password link has been sent to your email ", "OK");
            }
            if (string.IsNullOrEmpty(Email.Text))
            {
                await DisplayAlert("Error", "Enter email! ", "OK");
            }
            
        }
        private async void BtnBack_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LoginPage());
        }
    }
}