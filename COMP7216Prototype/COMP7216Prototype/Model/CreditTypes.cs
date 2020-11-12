using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Model
{
    public class CreditTypes
    {
        [PrimaryKey, AutoIncrement]
        public int creditTypeId { get; set; }
        public string creditType { get; set; }
    }
}
