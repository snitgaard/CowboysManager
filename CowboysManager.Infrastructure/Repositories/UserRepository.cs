using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Infrastructure.Repositories
{
        public class UserRepository : IUserRepository<User>
        {

            public User CreateUser(User user)
            {
                DBInitializer dbInit = new DBInitializer();

                string query = "INSERT INTO User('username', 'passwordHash', 'passwordSalt') " +
                    "VALUES (@username, @passwordHash, @passwordSalt)";
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                myCommand.Parameters.AddWithValue("@username", user.Username);
                myCommand.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                myCommand.Parameters.AddWithValue("@passwordSalt", user.PasswordSalt);
                myCommand.ExecuteNonQuery();
                dbInit.CloseConnection();
                return user;
            }
            /*public User GetUserById(long id)
            {

                DBInitializer dbInit = new DBInitializer();
                string query = @"SELECT * FROM User WHERE Id = " + id;
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                SQLiteDataReader r = myCommand.ExecuteReader();
                if(r.HasRows)
                {
                    var user = new User
                    {
                        Id = r.GetInt64(r.GetOrdinal("id")),
                        Username = r["Username"].ToString(),
                        PasswordHash = (byte[])r["PasswordHash"],
                        PasswordSalt = (byte[])r["PasswordSalt"]
                    };
                }

                dbInit.CloseConnection();
                return user;
            }
            */
            public IEnumerable<User> GetAllUsers()
            {
                List<User> UserList = new List<User>();
                DBInitializer dbInit = new DBInitializer();
                string query = @"SELECT * FROM User";
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                SQLiteDataReader r = myCommand.ExecuteReader();
                while (r.Read())
                {
                    var user = new User
                    {
                        Id = r.GetInt64(r.GetOrdinal("id")),
                        Username = r["Username"].ToString(),
                        PasswordHash = (byte[])r["PasswordHash"],
                        PasswordSalt = (byte[])r["PasswordSalt"]
                    };
                    UserList.Add(user);
                }
                dbInit.CloseConnection();
                return UserList;
            }
        }
    }
