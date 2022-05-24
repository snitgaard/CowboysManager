using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Core.Entities
{
    public class Platform
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }

        public string EncryptedPassword { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

    }
}
