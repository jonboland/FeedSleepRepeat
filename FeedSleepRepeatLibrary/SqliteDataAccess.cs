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
    public static class SqliteDataAccess
    {
        public static List<Baby> LoadBabies()
        {
            string sql = "SELECT * FROM Baby ORDER BY LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Baby>(sql, new DynamicParameters());
                return output.ToList();
            }
        }

        public static void CreateBaby(Baby baby, BabyDay babyDay, List<Activity> activities)
        {
            string babySql = "INSERT INTO Baby (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth); SELECT last_insert_rowid()";

            string babyDaySql = "INSERT INTO BabyDay (BabyId, Date, Weight, WetNappies, DirtyNappies) "
                + "VALUES (@BabyId, @Date, @Weight, @WetNappies, @DirtyNappies); SELECT last_insert_rowid()";

            string activitySql = "INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace) "
                + "VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            // TODO: Consider converting baby creation into a transaction
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Insert baby into Baby table and add Id to babyDay
                babyDay.BabyId = cnn.QueryFirst<int>(babySql, baby);
                // Insert babyDay into BabyDay table with BabyId as foreign key and assign Id to variable
                int babyDayId = cnn.QueryFirst<int>(babyDaySql, babyDay);
                // Insert activities into Activity table with BabyDayId as foreign key
                foreach (var activity in activities)
                {
                    activity.BabyDayId = babyDayId;
                    cnn.Execute(activitySql, activity);
                }
            }
        }

        // TODO: Consider combining with UpdateBabyDay
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
            // ON DELETE CASCADE is applied to FOREIGN KEY in BabyDay TABLE
            string sql = @"PRAGMA foreign_keys = ON; 
                           DELETE FROM Baby 
                           WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        public static List<BabyDay> LoadBabyDays(Baby currentBaby)
        {
            string sql = "SELECT * FROM BabyDay WHERE BabyId = @Id";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<BabyDay>(sql, currentBaby);
                return output.ToList();
            }
        }

        public static void CreateBabyDay(BabyDay babyDay, List<Activity> activities)
        {
            string babyDaySql = "INSERT INTO BabyDay (BabyId, Date, Weight, WetNappies, DirtyNappies) "
                + "VALUES (@BabyId, @Date, @Weight, @WetNappies, @DirtyNappies); SELECT last_insert_rowid()";

            string activitySql = "INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace) "
                + "VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Insert babyDay into BabyDay table with BabyId as foreign key and assign Id to variable
                int babyDayId = cnn.QueryFirst<int>(babyDaySql, babyDay);
                // Insert activities into Activity table with BabyDayId as foreign key
                foreach (var activity in activities)
                {
                    activity.BabyDayId = babyDayId;
                    cnn.Execute(activitySql, activity);
                }
            }
        }

        public static void UpdateBabyDay(BabyDay babyDay, List<Activity> activities)
        {
            string updatebabyDaySql = @"UPDATE BabyDay 
                                        SET Weight = @Weight, WetNappies = @WetNappies, DirtyNappies = @DirtyNappies 
                                        WHERE BabyId = @BabyId 
                                        AND Date = @Date";

            string deleteActivitySql = "DELETE FROM Activity WHERE BabyDayId = @Id";

            string insertActivitySql = @"INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace)
                                         VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(updatebabyDaySql, babyDay);
                cnn.Execute(deleteActivitySql, babyDay);
                cnn.Execute(insertActivitySql, activities);
            }
        }

        // TODO: Consider combining with LoadBabyDays
        public static List<Activity> LoadActivities(BabyDay currentBabyDay)
        {
            string sql = "SELECT * FROM Activity WHERE BabyDayId = @Id";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Activity>(sql, currentBabyDay);
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
