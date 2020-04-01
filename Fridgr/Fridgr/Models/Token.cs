using System;
namespace Fridgr.Models
{
    public class Token
    {
        public int id { get; set; }
        public string access { get; set; }
        public string error_desc { get; set; }
        public DateTime expiration { get; set; }

        public Token() { }
    }
}
