using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace COMP7216Prototype.Model
{
    public class CreditShares
    {
        [PrimaryKey, AutoIncrement]
        public int shareId { get; set; }
        public double creditAmount { get; set; }
        public string timeStampDate { get; set; }
        public string timeStampTime { get; set; }
        [Indexed]
        public int creditTypeId { get; set; }
        [Indexed]
        public int receiverUserId { get; set; }
        [Indexed]
        public int shareUserId { get; set; }
        [Indexed]
        public int requestId { get; set; }
        public string statement { get; set; }
    }
}
