
using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreditShareForm : ContentPage
	{
        public int receiverId;
        DataAccessLayer dal = new DataAccessLayer();
        public List<Customers> searchQuery { get; set; }
        public bool queryVisible { get; set; }
        public Customers loggedInUser { get; set; }
        public CreditShareForm (Customers _loggedInUser)
		{
			InitializeComponent ();
            loggedInUser = _loggedInUser;
            queryVisible = false;
            BindingContext = this;
		}

        public CreditShareForm(Customers _loggedInUser, CreditRequests request)
        {
            InitializeComponent();
            loggedInUser = _loggedInUser;
            entryCreditAmount.Text = request.creditAmount.ToString();
            string requestPhNum = dal.db.Query<Customers>($"SELECT * FROM Customers WHERE customerId={request.requesterId}").First().phoneNumber;
            entryReceiver.Text = requestPhNum;
            entryCreditAmount.Text = request.creditAmount.ToString();
            pickerCreditType.SelectedIndex = request.creditTypeId - 1;
            queryVisible = false;
            BindingContext = this;
        }

        public async void BtnConfirmation_Clicked(object sender, EventArgs e) 
        {

            var receviverAccount = entryReceiver.Text;
            var creditType = pickerCreditType.SelectedItem;
            var amount = Convert.ToDouble(entryCreditAmount.Text);

            bool answer = await DisplayAlert( "Transfer Credit?","Click Yes To continue" , "Yes", "No" );
            

            if (answer == true)
            {
                //send logic
                //grab the requester's id
                //send credits
                var receiverCredits = dal.db.Query<Credits>($"SELECT * FROM Credits WHERE customerId={receiverId} AND creditTypeId={pickerCreditType.SelectedIndex+1}").First();
                var senderCredits = dal.db.Query<Credits>($"SELECT * FROM Credits WHERE customerId={loggedInUser.customerId} AND creditTypeId={pickerCreditType.SelectedIndex + 1}").First();
                double receiverAmount = Convert.ToDouble(receiverCredits.amount);
                double senderAmount = Convert.ToDouble(senderCredits.amount);
                double newSenderAmount = senderAmount - amount;
                double newReceiverAmount = receiverAmount + amount;
                if ((newSenderAmount) < 0) { await DisplayAlert("Error","Insufficient balance!","OK");return; }
                dal.db.Query<Credits>($"UPDATE Credits SET amount={newSenderAmount} WHERE customerId={loggedInUser.customerId} AND creditTypeId={pickerCreditType.SelectedIndex + 1}");
                dal.db.Query<Credits>($"UPDATE Credits SET amount={newReceiverAmount} WHERE customerId={receiverId} AND creditTypeId={pickerCreditType.SelectedIndex + 1}");
                //log the share
                int thisReqId = -1;
                try
                {
                    var request = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE requesterId={receiverId} AND creditAmount<={senderAmount} AND requestAccepted=False").First();
                    dal.db.Query<CreditRequests>($"UPDATE CreditRequests SET requestAccepted=True WHERE requesterId={receiverId} AND creditAmount<={senderAmount} AND requestAccepted=False");
                    thisReqId = request.requestId;
                }
                catch (Exception) { }
                dal.db.Insert(new CreditShares
                {
                    creditAmount = amount,
                    creditTypeId = pickerCreditType.SelectedIndex+1,
                    receiverUserId = receiverId,
                    requestId = thisReqId,
                    shareUserId = loggedInUser.customerId,
                    timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    timeStampTime = DateTime.Now.ToString("hh:mm")
                });

                await DisplayAlert("Transfer", "Complete", "OK");
                entryReceiver.Text = "";
                entryCreditAmount.Text = "";
                queryVisible = false;
                await Navigation.PopAsync();

            }
            else
            {
                return;
            }

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void entryReceiver_TextChanged(object sender, TextChangedEventArgs e)
        {
            queryVisible = true;
            searchQuery = dal.db.Query<Customers>($"SELECT * FROM Customers WHERE phoneNumber LIKE '{e.NewTextValue}%' AND NOT customerId={loggedInUser.customerId}");
            BindingContext = null;
            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            entryReceiver.Text = searchQuery[e.ItemIndex].phoneNumber;
            receiverId = searchQuery[e.ItemIndex].customerId;
            queryVisible = false;
            BindingContext = null;
            BindingContext = this;
        }
    }
}