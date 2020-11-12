using COMP7216Prototype.Controller;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP7216Prototype.Model
{
    public class CreditRequests
    {

        [PrimaryKey, AutoIncrement]
        public int requestId { get; set; }
        public double creditAmount { get; set; }
        public string timeStampDate { get; set; }
        public string timeStampTime { get; set; }
        public bool requestAccepted { get; set; }
        [Indexed]
        public int creditTypeId { get; set; }
        [Indexed]
        public int requesterId { get; set; }
        [Indexed]
        public int shareUserId { get; set; }
        [Indexed]
        public int shareId { get; set; }
        public string statement { get; set; }
    }
}
