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
        public MainMenuPage()
        {
            InitializeComponent();
        }
        private async void BtnRequest_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void BtnShare_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreditShareForm());
        }
        private async void BtnHistory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogSearchPage());
        }
    }

}