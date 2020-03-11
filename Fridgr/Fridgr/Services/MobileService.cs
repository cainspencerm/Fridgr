using System;
using Fridgr.Models;
namespace Fridgr.Services
{
    public class MobileService
    {
        private readonly MongoHelper<Mobile> _mob;
        public MobileService()
        {
            _mob = new MongoHelper<Mobile>();
        }

        public void Create(Mobile mob)
        {
            // save: insert if not present (?)
        }
    }
}
