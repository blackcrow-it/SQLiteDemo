using DataSQLite.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DataSQLite.Handle
{
    class DataHandle
    {
        public static void AddData(User obj)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=UserData.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (@phone, @name, @email);";
                insertCommand.Parameters.AddWithValue("@phone", obj.phone);
                insertCommand.Parameters.AddWithValue("@name", obj.name);
                insertCommand.Parameters.AddWithValue("@email", obj.email);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
