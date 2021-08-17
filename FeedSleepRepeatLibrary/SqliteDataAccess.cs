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
                var output = cnn.Query<Baby>("SELECT * FROM Baby ORDER BY LastName", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void CreateBaby(Baby baby, BabyDay babyDay)
        {
            string babySql = @"INSERT INTO Baby (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth); SELECT last_insert_rowid()";

            string babyDaySql = @"INSERT INTO BabyDay (BabyId, Date, Weight, WetNappies, DirtyNappies) VALUES (@BabyId, @Date, @Weight, @WetNappies, @DirtyNappies)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                babyDay.BabyId = cnn.QueryFirst<int>(babySql, baby);

                cnn.Execute(babyDaySql, babyDay);
            }
        }

        public static void UpdateBaby(Baby baby)
        {
            string sql = "UPDATE Baby SET DateOfBirth = @DateOfBirth WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        public static void DeleteBaby(Baby baby)
        {
            string sql = "DELETE FROM Baby WHERE FirstName = @FirstName AND LastName = @LastName";

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
