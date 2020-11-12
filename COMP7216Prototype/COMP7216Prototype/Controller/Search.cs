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
        }

        public List<CreditRequests> SearchRequests(string query, Customers user)
        {
            var results = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE timeStampDate LIKE '{query}%' AND requesterId={user.customerId}");
            return results;
        }

        public List<CreditShares> SearchShares(string query, Customers user)
        {
            var results = dal.db.Query<CreditShares>($"SELECT * FROM CreditShares WHERE timeStampDate LIKE '{query}%' AND shareUserId={user.customerId}");
            return results;
        }

        public List<CreditRequests> ViewRequests(Customers user)
        {
            var results = dal.db.Query<CreditRequests>($"SELECT * FROM CreditRequests WHERE requesterId={user.customerId}");
            return results;
        }

        public List<CreditShares> ViewShares(Customers user)
        {
            var results = dal.db.Query<CreditShares>($"SELECT * FROM CreditShares WHERE shareUserId={user.customerId}");
            return results;
        }

        
        
    }
}
