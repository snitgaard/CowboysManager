using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Infrastructure.Repositories
{
        public class PlatformRepository : IPlatformRepository<Platform>
        {
            public Platform CreatePlatform(Platform platform)
            {
                DBInitializer dbInit = new DBInitializer();
                string query = "INSERT INTO Platform('name', 'Username', 'encryptedpassword', 'userId') " +
                               "VALUES (@name, @username, @encryptedpassword, @UserId)";
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                myCommand.Parameters.AddWithValue("@name", platform.Name);
                myCommand.Parameters.AddWithValue("@username", platform.Username);
                myCommand.Parameters.AddWithValue("@encryptedpassword", platform.EncryptedPassword);
                myCommand.Parameters.AddWithValue("@userId", platform.UserId);
                myCommand.ExecuteNonQuery();
                dbInit.CloseConnection();
                return platform;
            }

            public IEnumerable<Platform> GetAllPlatforms()
            {
                List<Platform> PlatformList = new List<Platform>();
                DBInitializer dbInit = new DBInitializer();
                string query = @"SELECT * FROM Platform";
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                SQLiteDataReader r = myCommand.ExecuteReader();
                while (r.Read())
                {
                    var platform = new Platform
                    {
                        Id = r.GetInt64(r.GetOrdinal("id")),
                        Name = r["Name"].ToString(),
                        Username = r["Username"].ToString(),
                        EncryptedPassword = r["EncryptedPassword"].ToString(),
                        UserId = r.GetInt64(r.GetOrdinal("userId")),

                    };
                    PlatformList.Add(platform);
                }
                dbInit.CloseConnection();
                return PlatformList;
            }

            public IEnumerable<Platform> GetAllPlatformsByUserId(long userid)
            {
                List<Platform> PlatformList = new List<Platform>();
                DBInitializer dbInit = new DBInitializer();
                string query = @"SELECT * FROM Platform Where userId =" + userid;
                SQLiteCommand myCommand = new SQLiteCommand(query, dbInit.myConnection);
                dbInit.OpenConnection();
                SQLiteDataReader r = myCommand.ExecuteReader();
                while (r.Read())
                {
                    var platform = new Platform
                    {
                        Id = r.GetInt64(r.GetOrdinal("id")),
                        Name = r["Name"].ToString(),
                        Username = r["Username"].ToString(),
                        EncryptedPassword = r["EncryptedPassword"].ToString(),
                        UserId = r.GetInt64(r.GetOrdinal("userId")),

                    };
                    PlatformList.Add(platform);
                }
                dbInit.CloseConnection();
                return PlatformList;
            }
        }
    }
