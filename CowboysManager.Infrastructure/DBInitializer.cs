using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.Infrastructure
{
    public class DBInitializer
    {
        public SQLiteConnection myConnection;
        public DBInitializer()
        {
            myConnection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                Console.WriteLine("Database created");
            }

        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
