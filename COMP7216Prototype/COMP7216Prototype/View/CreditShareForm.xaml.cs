
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreditShareForm : ContentPage
	{
 public int receiver;
        
		public CreditShareForm ()
		{
			InitializeComponent ();
            
		}





        private void BtnReceiver_Clicked(object sender, EventArgs e)
        { 



            int x = Convert.ToInt32(entryReceiver.Text);
          

          

            if ( x <= 9999999 && x >= 1000 )
            {
                DisplayAlert("Receiver Exists", "click ok to close", "OK");
            }  
            else
            {
            DisplayAlert("Receiver Does Not Exists", "click ok to close", "OK");
            }
            

        }

        public async void BtnConfirmation_Clicked(object sender, EventArgs e) 
        {

            var receviverAccount = entryReceiver.Text;
            var creditType = pickerCreditType.SelectedItem;
            var amount = entryCreditAmount;

            bool answer = await DisplayAlert( "Transfer Credit?","Click Yes To continue" , "Yes", "No" );
            


            if (answer == true)
            {
                await DisplayAlert("Transfer", "Complete", "OK");

            }
            else
            {
                return;
            }

        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}