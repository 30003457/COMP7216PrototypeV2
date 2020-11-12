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
        public MainPage()
        {
            InitializeComponent();
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
            }
        }

        //Sends the credit request to the selected creditor and resets the entry boxes
        async void SendRequest(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Your credit request has been sent to the selected creditor," +
                " you will be alerted when the creditor has responded to your request", "OK");

            SendRequestButton.SetValue(IsVisibleProperty, false);

            CreditorEntryBox.Text = "";
            CreditTypePicker.SelectedItem = null;
            CreditAmountEntryBox.Text = "";
        }
    }
}
