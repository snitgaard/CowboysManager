
using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CowboysManager.Core.Services
{
    public class AuthService : IAuthentication
    {
        static Byte[] secretBytes = new byte[40];

            static AuthService()
            {
                Random rand = new Random();
                rand.NextBytes(secretBytes);
            }
            public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }

            public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i]) return false;
                    }
                }
                return true;
            }

            public User PasswordHasher(string username, string password)
            {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Username = username,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return user;

        }
    }
    }

