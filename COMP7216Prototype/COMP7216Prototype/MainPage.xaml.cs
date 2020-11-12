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
        public string status { get; set; }
        public MainPage()
        {
            InitializeComponent();
            var results = dal.db.Query<CreditTypes>("SELECT * FROM CreditTypes");
            foreach (var item in results)
            {
                status += $"{item.creditTypeId} - {item.creditType}\n";
            }
            BindingContext = null;
            BindingContext = this;
        }

        //Validates the form, checking that all entry boxes are filled and then confirms the form allowing the user to send their request
        async void ConfrimButton(object sender, EventArgs e)
        {
            var creditorValue = CreditorEntryBox.Text;
            var dataTypeValue = CreditTypePicker.SelectedItem;
            var amountValue = CreditAmountEntryBox.Text;
            
            if(creditorValue == "" || dataTypeValue == null || amountValue == "")
            {
                await DisplayAlert("Alert", "Please filled all fields before confirming", "OK");
            }
            else
            {
                await DisplayAlert("Success", "The request from has been validated, click the Send Request" +
                    " button to send your credit request to the selected creditors", "OK");

                SendRequestButton.SetValue(IsVisibleProperty, true);

                //int amount = int.Parse(amountValue.ToString());
                //int creditType = int.Parse(dataTypeValue.ToString());
                //DateTime time = DateTime.Now.ToLocalTime();

                //cr.creditAmount = amount;
                //cr.timeStampDate = DateTime.Now.ToString();
                //cr.timeStampTime = time.ToString();
                //cr.creditTypeId = creditType;

                ////Fix
                //cr.requesterId = 0;
                //cr.requesterId = 1;
                //cr.shareUserId = 2;
                //cr.shareId = 3;
                //cr.statement = "Fix";
            }
        }

        //Sends the credit request to the selected creditor and resets the entry boxes
        async void SendRequest(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Your credit request has been sent to the selected creditor," +
                " you will be alerted when the creditor has responded to your request", "OK");

            SendRequestButton.SetValue(IsVisibleProperty, false);

            double amount = Convert.ToDouble(CreditAmountEntryBox.Text);

            cr.creditAmount = amount;
            cr.timeStampDate = DateTime.Now.ToString("dd/MM/yyyy");
            cr.timeStampTime = DateTime.Now.ToString("hh:mm");

            //Fix
            cr.requesterId = 2;
            cr.shareUserId = 1;
            cr.creditTypeId = 3;
            cr.creditAmount = amount;
            cr.requestAccepted = false;
            cr.shareId = -1;
            
            //Logging
            dal.db.Insert(cr);

            //Resets entry boxes
            CreditorEntryBox.Text = "";
            CreditTypePicker.SelectedItem = null;
            CreditAmountEntryBox.Text = "";


        }
    }
}
