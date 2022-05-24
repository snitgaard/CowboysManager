using CowboysManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Core.Interfaces
{
    public interface IUserRepository<T>
    {
        User CreateUser(User user);
        IEnumerable<User> GetAllUsers();
    }
}
