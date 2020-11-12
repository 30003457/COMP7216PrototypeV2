using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
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
    public partial class ViewRequests : ContentPage
    {
        public List<CreditRequests> Requests{ get; set; }
        private DataAccessLayer dal;
        private Customers User;
        public ViewRequests(Customers user)
        {
            InitializeComponent();
            User = user;
            dal = new DataAccessLayer();
            Requests = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE shareUserId={user.customerId}");
            foreach (var request in Requests)
            {
                string requesterName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.requesterId}").First().firstName}";
                string shareUserName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.shareUserId}").First().firstName}";
                string creditTypeName = $"{dal.db.Query<CreditTypes>($"SELECT creditType FROM CreditTypes WHERE creditTypeId={request.creditTypeId}").First().creditType}";
                request.statement = $"{requesterName} requested {request.creditAmount} {creditTypeName} from you.";
            }
            BindingContext = this;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CreditShareForm(User, (CreditRequests)e.Item));
        }
    }
}