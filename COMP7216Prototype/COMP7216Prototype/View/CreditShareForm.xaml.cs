
using System;
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

            var action = await DisplayActionSheet("Account: " + entryReceiver.Text, "Credit Type: " + pickerCreditType.SelectedItem.ToString(),  "Amount: " + entryCreditAmount.Text, "Transfer Credit?" , "Yes", "No" );
            Console.WriteLine("Save Data: "+ action);


            await DisplayAlert("Transfer", "Complete", "OK");


        }

    }
}