using System;
using MongoDB.Bson;

namespace Fridgr.Models
{
    public class Mobile
    {
        public Guid ID { get; set; }
        public int MobileID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
