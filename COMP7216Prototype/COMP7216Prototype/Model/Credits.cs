using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Model
{
    public class Credits
    {
        [PrimaryKey, AutoIncrement]
        public int creditId { get; set; }
        public double amount { get; set; }
        [Indexed]
        public int creditTypeId { get; set; }
        [Indexed]
        public int customerId { get; set; }
    }
}
