using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace COMP7216Prototype
{
    public partial class MainPage : ContentPage
    {
        DataAccessLayer dal = new DataAccessLayer();
        CreditRequests cr = new CreditRequests();
        public Customers loggedInUser { get; set; }
        public List<Customers> searchQuery { get; set; }
        public bool queryVisible { get; set; }
        int creditorId;
        public MainPage(Customers _loggedInUser)
        {
            InitializeComponent();
            loggedInUser = _loggedInUser;
            queryVisible = false;
            BindingContext = this;
        }

        //Validates the form, checking that all entry boxes are filled and then confirms the form allowing the user to send their request
        async void ConfrimButton(object sender, EventArgs e)
        {
            int creditTypeIndex = -1;
            var creditorValue = CreditorEntryBox.Text;
            creditTypeIndex = CreditTypePicker.SelectedIndex;
            var amountValue = CreditAmountEntryBox.Text;
            
            if(creditorValue == "" || creditTypeIndex == -1 || amountValue == "")
            {
                await DisplayAlert("Alert", "Please filled all fields before confirming", "OK");
            }
            else
            {
                await DisplayAlert("Success", "The request from has been validated, click the Send Request" +
                    " button to send your credit request to the selected creditors", "OK");

                SendRequestButton.SetValue(IsVisibleProperty, true);
            }
        }

        //Sends the credit request to the selected creditor and resets the entry boxes
        async void SendRequest(object sender, EventArgs e)
        {
            SendRequestButton.SetValue(IsVisibleProperty, false);

            double amount = Convert.ToDouble(CreditAmountEntryBox.Text);

            //copied logic
            //send logic
            //grab the creditor's id
            //send request
            //log the request
            dal.db.Insert(new CreditRequests
            {
                creditAmount = amount,
                creditTypeId = CreditTypePicker.SelectedIndex + 1,
                requesterId = loggedInUser.customerId,
                shareUserId = creditorId,
                timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                timeStampTime = DateTime.Now.ToString("hh:mm"),
                requestAccepted = false
            });

            //Resets entry boxes
            CreditorEntryBox.Text = "";
            CreditTypePicker.SelectedItem = null;
            CreditAmountEntryBox.Text = "";

            await DisplayAlert("Success", "Your credit request has been sent to the selected creditor," +
                " you will be alerted when the creditor has responded to your request", "OK");
            await Navigation.PopAsync();

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CreditorEntryBox.Text = searchQuery[e.ItemIndex].phoneNumber;
            creditorId = searchQuery[e.ItemIndex].customerId;
            queryVisible = false;
            BindingContext = null;
            BindingContext = this;
        }

        private void CreditorEntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            queryVisible = true;
            searchQuery = dal.db.Query<Customers>($"SELECT * FROM Customers WHERE phoneNumber LIKE '{e.NewTextValue}%' AND NOT customerId={loggedInUser.customerId}");
            BindingContext = null;
            BindingContext = this;
        }
    }
}
