using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Model
{
    public class Customers
    {
        [PrimaryKey, AutoIncrement]
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }

    }
}
