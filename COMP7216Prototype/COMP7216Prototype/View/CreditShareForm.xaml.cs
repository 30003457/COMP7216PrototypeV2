
using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
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
        DataAccessLayer dal = new DataAccessLayer();
        
		public CreditShareForm ()
		{
			InitializeComponent ();
            
		}





        private void BtnReceiver_Clicked(object sender, EventArgs e)
        {



            int x = Convert.ToInt32(entryReceiver.Text);




            if (x <= 9999999 && x >= 1000)
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
                //send logic
                //grab the requester's id
                //send credits
                dal.db.Insert(new Credits
                {
                    amount = 0,
                    creditTypeId = 3,
                    customerId = 2
                });
                var results = dal.db.Query<Credits>($"SELECT * FROM Credits WHERE customerId=2");
                Credits result;
                result = results[0];
                result.amount += Convert.ToDouble(entryCreditAmount.Text);
                result.creditTypeId = 3; //data
                dal.db.Update(result);
                //log share
                var request = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE requesterId=2");
                CreditRequests cr = request[request.Count - 1];
                cr.requestAccepted = true;
                dal.db.Update(cr);
                dal.db.Insert(new CreditShares
                {
                    creditAmount = Convert.ToDouble(entryCreditAmount.Text),
                    creditTypeId = 3,
                    receiverUserId = 2,
                    requestId = cr.requestId,
                    shareUserId = 1,
                    timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    timeStampTime = DateTime.Now.ToString("hh:mm")
                });

                await DisplayAlert("Transfer", "Complete", "OK");
                entryReceiver.Text = "";
                entryCreditAmount.Text = "";

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