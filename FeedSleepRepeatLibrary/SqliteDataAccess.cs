using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace FeedSleepRepeatLibrary
{
    public class SqliteDataAccess
    {
        public static List<Baby> LoadBabies()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Baby>("SELECT * FROM BabyTable ORDER BY LastName", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void CreateBaby(Baby baby)
        {
            string sql = "INSERT INTO BabyTable (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        public static void UpdateBaby(Baby baby)
        {
            string sql = "UPDATE BabyTable SET DateOfBirth = @DateOfBirth WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        public static void DeleteBaby(Baby baby)
        {
            string sql = "DELETE FROM BabyTable WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
