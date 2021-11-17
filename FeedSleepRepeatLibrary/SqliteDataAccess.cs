using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace FeedSleepRepeatLibrary
{
    public static class SqliteDataAccess
    {
        /// <summary>
        /// Loads all the babys from the database.
        /// </summary>
        /// <returns>A list of Baby instances.</returns>
        public static List<Baby> LoadBabies()
        {
            string sql = "SELECT * FROM Baby ORDER BY LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Baby>(sql, new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Inserts a new baby and the associated baby day and activities into the database.
        /// </summary>
        /// <param name="baby">The Baby instance to insert.</param>
        /// <param name="babyDay">The BabyDay instance to insert, including any associated activities.</param>
        public static void CreateBaby(Baby baby, BabyDay babyDay)
        {
            string babySql = "INSERT INTO Baby (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth); SELECT last_insert_rowid()";

            string babyDaySql = @"INSERT INTO BabyDay (BabyId, Date, Weight, WetNappies, DirtyNappies) 
                                  VALUES (@BabyId, @Date, @Weight, @WetNappies, @DirtyNappies); SELECT last_insert_rowid()";

            string activitySql = @"INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace) 
                                   VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    // Insert baby into Baby table and add Id to babyDay
                    babyDay.BabyId = cnn.QueryFirst<int>(babySql, baby, trans);
                    // Insert babyDay into BabyDay table with BabyId as foreign key and assign Id to variable
                    int babyDayId = cnn.QueryFirst<int>(babyDaySql, babyDay, trans);
                    // Insert activities into Activity table with BabyDayId as foreign key
                    foreach (var activity in babyDay.Activities)
                    {
                        activity.BabyDayId = babyDayId;
                        cnn.Execute(activitySql, activity, trans);
                    }
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// Sets the date of birth for the given baby.
        /// </summary>
        /// <param name="baby">The Baby instance to update.</param>
        public static void UpdateDateOfBirth(Baby baby)
        {
            string sql = "UPDATE Baby SET DateOfBirth = @DateOfBirth WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        /// <summary>
        /// Deletes the supplied baby from the database. All associated baby days
        /// and activities are also deleted via foreign key.
        /// </summary>
        /// <param name="baby">The baby instance to delete.</param>
        public static void DeleteBaby(Baby baby)
        {
            // ON DELETE CASCADE is applied to FOREIGN KEY in BabyDay and Activity
            string sql = @"PRAGMA foreign_keys = ON; 
                           DELETE FROM Baby 
                           WHERE FirstName = @FirstName AND LastName = @LastName";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, baby);
            }
        }

        /// <summary>
        /// Loads all the baby days for the given baby from the database.
        /// </summary>
        /// <param name="currentBaby">The baby whose day instances should be loaded.</param>
        /// <returns>A list of BabyDay instances.</returns>
        public static List<BabyDay> LoadBabyDays(Baby currentBaby)
        {
            string sql = "SELECT * FROM BabyDay WHERE BabyId = @Id";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<BabyDay>(sql, currentBaby);
                return output.ToList();
            }
        }

        /// <summary>
        /// For an existing baby, inserts a new baby day and any associated activities into the database. 
        /// </summary>
        /// <param name="babyDay">The BabyDay instance to insert, including any associated activities.</param>
        public static void CreateBabyDay(BabyDay babyDay)
        {
            string babyDaySql = @"INSERT INTO BabyDay (BabyId, Date, Weight, WetNappies, DirtyNappies) 
                                  VALUES (@BabyId, @Date, @Weight, @WetNappies, @DirtyNappies); SELECT last_insert_rowid()";

            string activitySql = @"INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace) 
                                   VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    // Insert babyDay into BabyDay table with BabyId as foreign key and assign Id to variable
                    int babyDayId = cnn.QueryFirst<int>(babyDaySql, babyDay, trans);
                    // Insert activities into Activity table with BabyDayId as foreign key
                    foreach (var activity in babyDay.Activities)
                    {
                        activity.BabyDayId = babyDayId;
                        cnn.Execute(activitySql, activity, trans);
                    }
                    trans.Commit();
                }

            }
        }

        /// <summary>
        /// Updates the given baby day's values. Adds any new activities associated with the day and deletes any that have been removed.
        /// </summary>
        /// <param name="babyDay">The BabyDay instance to be updated.</param>
        public static void UpdateBabyDay(BabyDay babyDay)
        {
            string updateBabyDaySql = "UPDATE BabyDay SET Weight = @Weight, WetNappies = @WetNappies, DirtyNappies = @DirtyNappies WHERE Id = @Id";

            string deleteActivitySql = "DELETE FROM Activity WHERE BabyDayId = @BabyDayId AND Id NOT IN @ActivityIds";

            string insertActivitySql = @"INSERT INTO Activity (BabyDayId, ActivityType, Start, End, FeedType, FeedAmount, SleepPlace)
                                         VALUES (@BabyDayId, @ActivityType, @Start, @End, @FeedType, @FeedAmount, @SleepPlace)";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                List<int> activityIds = babyDay.Activities.Select(a => a.Id).ToList();
                DynamicParameters deleteParams = new();
                deleteParams.Add("BabyDayId", babyDay.Id);
                deleteParams.Add("ActivityIds", activityIds);

                // ROWIDs assigned via Sqlite AUTOINCREMENT begin at 1
                List<Activity> newActivities = babyDay.Activities.Where(a => a.Id == 0).ToList();

                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    cnn.Execute(updateBabyDaySql, babyDay, trans);
                    cnn.Execute(deleteActivitySql, deleteParams, trans);
                    cnn.Execute(insertActivitySql, newActivities, trans);
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// Load the activities for the given baby day from the database.
        /// </summary>
        /// <param name="currentBabyDay">The baby day for which instances should be loaded.</param>
        /// <returns>A list of Activity instances.</returns>
        public static List<Activity> LoadActivities(BabyDay currentBabyDay)
        {
            string sql = "SELECT * FROM Activity WHERE BabyDayId = @Id";

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Activity>(sql, currentBabyDay);
                return output.ToList();
            }
        }

        /// <summary>
        /// Loads the connection string used to access the database.
        /// </summary>
        /// <param name="id">The name of the connection string to load.</param>
        /// <returns>The connection string, detailing data source and version.</returns>
        private static string LoadConnectionString(string id = "DataDir")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
