using COMP7216Prototype.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogSearchPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
