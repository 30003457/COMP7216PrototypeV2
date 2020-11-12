using COMP7216Prototype.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Miracast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        Register regController;
        public RegisterPage()
        {
            InitializeComponent();
            regController = new Register();
            BindingContext = regController;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var valid = regController.RegisterClicked();
            if (valid == false) { await DisplayAlert("Error", "Form must not be blank and passwords must match.", "OK"); return; }
            else await DisplayAlert("Successful","Registration successful, you may now login.","OK");
            await Navigation.PopAsync();
        }
    }
}