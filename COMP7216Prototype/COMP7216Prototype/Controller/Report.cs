using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Controller
{
    public class Report
    {
        DataAccessLayer dal;
        public List<string> searchResults { get; set; }
        public List<CreditRequests> Requests { get; set; }
        public List<CreditShares> Shares { get; set; }
        public Customers user { get; set; }
        public Report(Customers _user)
        {
            dal = new DataAccessLayer();
            user = _user;
        }

        public void SetRequestStatements()
        {
            searchResults = new List<string>();
            //Requests = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE requesterId={user.customerId}");
            foreach (var request in Requests)
            {
                string requesterName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.requesterId}")[0].firstName}";
                string shareUserName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.shareUserId}")[0].firstName}";
                string creditTypeName = $"{dal.db.Query<CreditTypes>($"SELECT creditType FROM CreditTypes WHERE creditTypeId={request.creditTypeId}")[0].creditType}";
                request.statement = $"You requested {request.creditAmount} {creditTypeName} from {shareUserName}.";
                if (request.requestAccepted == true) request.statement += ". The request was accepted.";

                searchResults.Add(request.statement);
            }
        }

        public void SetShareStatements()
        {
            searchResults = new List<string>();

            //Shares = dal.db.Query<CreditShares>($"SELECT * FROM CreditShares WHERE shareUserId={user.customerId}");
            foreach (var share in Shares)
            {
                string receiverName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={share.receiverUserId}")[0].firstName}";
                string shareUserName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={share.shareUserId}")[0].firstName}";
                string creditTypeName = $"{dal.db.Query<CreditTypes>($"SELECT creditType FROM CreditTypes WHERE creditTypeId={share.creditTypeId}")[0].creditType}";
                share.statement = $"Shared {share.creditAmount} {creditTypeName} to {receiverName}";
                searchResults.Add(share.statement);

            }
        }
    }
}
