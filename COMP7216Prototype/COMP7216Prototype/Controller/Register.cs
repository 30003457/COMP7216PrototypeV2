using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace COMP7216Prototype.Controller
{
    public class Register
    {
        public Customers newCustomer { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }
        private DataAccessLayer dal;
        public Register()
        {
            dal = new DataAccessLayer();
            newCustomer = new Customers();
        }

        public bool RegisterClicked()
        {
            if (string.IsNullOrEmpty(newCustomer.address) == true ||
                string.IsNullOrEmpty(newCustomer.email) == true ||
                string.IsNullOrEmpty(newCustomer.firstName) == true ||
                string.IsNullOrEmpty(newCustomer.lastName) == true ||
                string.IsNullOrEmpty(newCustomer.phoneNumber) == true) { return false; }
            if (password1 != password2 || string.IsNullOrEmpty(password1) == true) { return false; }


            newCustomer.password = password1;
            dal.db.Insert(newCustomer);
            return true;
        }
    }
}
