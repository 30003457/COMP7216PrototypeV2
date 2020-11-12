using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Controller
{
    public class Report
    {
        DataAccessLayer dal;
        public List<CreditRequests> Requests { get; set; }
        public List<CreditShares> Shares { get; set; }
        public Report()
        {
            dal = new DataAccessLayer();
        }

        public void SetRequestStatements()
        {
            foreach (var request in Requests)
            {
                string requesterName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.requesterId}")[0].firstName}";
                string shareUserName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={request.shareUserId}")[0].firstName}";
                string creditTypeName = $"{dal.db.Query<CreditTypes>($"SELECT creditType FROM CreditTypes WHERE creditTypeId={request.creditTypeId}")[0].creditType}";
                string acceptedOrDeclined;
                if (request.requestAccepted == false) acceptedOrDeclined = "declined.";
                else acceptedOrDeclined = "accepted.";
                request.statement = $"{requesterName} requested {request.creditAmount} {creditTypeName} from {shareUserName}\n" +
                    $"The request was {acceptedOrDeclined}";
            }
        }

        public void SetShareStatements()
        {
            foreach (var share in Shares)
            {
                string receiverName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={share.receiverUserId}")[0].firstName}";
                string shareUserName = $"{dal.db.Query<Customers>($"SELECT firstName FROM Customers WHERE customerId={share.shareUserId}")[0].firstName}";
                string creditTypeName = $"{dal.db.Query<CreditTypes>($"SELECT creditType FROM CreditTypes WHERE creditTypeId={share.creditTypeId}")[0].creditType}";
                share.statement = $"{shareUserName} shared {share.creditAmount} {creditTypeName} to {receiverName}";
            }
        }
    }
}
