using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Model
{
    public class UserTypes
    {
        [PrimaryKey, AutoIncrement]
        public int userTypeId { get; set; }
        public string userType { get; set; }
    }
}
