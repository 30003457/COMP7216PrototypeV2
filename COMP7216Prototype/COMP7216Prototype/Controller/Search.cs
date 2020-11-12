using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using COMP7216Prototype.Model;
using SQLite;
using Xamarin.Forms;

namespace COMP7216Prototype.Controller
{
    public class Search
    {
        public DataAccessLayer dal;
        public Search()
        {
            dal = new DataAccessLayer();
            //CreateDummyTables();
        }

        public List<CreditRequests> SearchRequests(string query)
        {
            var results = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE timeStampDate='{query}'");
            return results;
        }

        public List<CreditShares> SearchShares(string query)
        {
            var results = dal.db.Query<CreditShares>($"SELECT * FROM CreditShares WHERE timeStampDate='{query}'");
            return results;
        }

        public List<CreditRequests> ViewRequests()
        {
            var results = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests");
            return results;
        }

        public List<CreditShares> ViewShares()
        {
            var results = dal.db.Query<CreditShares>($"SELECT * FROM CreditShares");
            return results;
        }

        private void CreateDummyTables()
        {
            //customers
            dal.db.CreateTable<Customers>();
            dal.db.Insert(new Customers
            {
                firstName = "Bob",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "bob.crawford@email.com",
                password = "123123123",
                phoneNumber = "123-123123"
            });

            dal.db.Insert(new Customers
            {
                firstName = "Joe",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "joe.crawford@email.com",
                password = "123123123",
                phoneNumber = "123-123123"
            });

            //credit types
            dal.db.CreateTable<CreditTypes>();
            dal.db.Insert(new CreditTypes
            {
                creditType = "dollars"
            });
            dal.db.Insert(new CreditTypes
            {
                creditType = "minutes"
            });
            dal.db.Insert(new CreditTypes
            {
                creditType = "GB"
            });
            dal.db.Insert(new CreditTypes
            {
                creditType = "texts"
            });

            //credits
            dal.db.CreateTable<Credits>();
            dal.db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 1,
                customerId = 1
            });

            //credit requests
            dal.db.CreateTable<CreditRequests>();
            dal.db.Insert(new CreditRequests
            {
                creditAmount = 20,
                creditTypeId = 1,
                requestAccepted = true,
                requesterId = 2,
                shareId = 1,
                shareUserId = 1,
                timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                timeStampTime = DateTime.Now.ToString("hh:mm")
            });

            //credit shares
            dal.db.CreateTable<CreditShares>();
            dal.db.Insert(new CreditShares
            {
                creditAmount = 20,
                creditTypeId = 1,
                receiverUserId = 2,
                requestId = -1,
                shareUserId = 1,
                timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                timeStampTime = DateTime.Now.ToString("hh:mm")
            });
        }
    }
}
