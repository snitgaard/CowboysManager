using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository<User> _userRepo;

        public UserService(IUserRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public User CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User is missing");
            }
            if (_userRepo.GetAllUsers().FirstOrDefault(u => u.Username == user.Username) != null)
            {
                throw new InvalidOperationException("This User already exists");
            }
            if (!IsValidUser(user))
            {
                throw new ArgumentException("Invalid user property");
            }

            return _userRepo.CreateUser(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            if (users == null)
            {
                throw new ArgumentException("No users found");
            }
            return users;
        }
        public bool IsValidUser(User user)
        {
            if(!string.IsNullOrEmpty(user.Username) && user.Username.Length <= 16 && user.Password.Length >= 8)
            {
                return true;
            }
            return false;
        }
    }
}
