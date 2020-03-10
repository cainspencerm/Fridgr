using System;
namespace Fridgr.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User() { }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public bool checkLogin()
        {
            if (this.username != null && this.password != null)
            {
                return true;
            }
            else return false;
        }
    }
}
