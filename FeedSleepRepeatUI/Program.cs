using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FeedSleepRepeatLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FeedSleepRepeatUI
{
    static class Program
    {
        static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static readonly string DataDir = Path.Combine(LocalAppData, Constants.DataFolder);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            CreateDataDirIfNotPresent();
            CopyDatabaseToDataDirIfNotPresent();
            AppDomain.CurrentDomain.SetData("DataDirectory", DataDir);

            IHost host = InitializeHost();
            FeedForm feedForm = host.Services.GetRequiredService<FeedForm>();
            Application.Run(feedForm);

            LogClosureAndFlush();
        }

        /// <summary>
        /// If fatal error occurs, shows message box, logs details, and exits application when user clicks OK.
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(
                Constants.FatalErrorOccured + e.Exception.Message,
                Constants.FatalErrorCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);

            Log.Fatal(e.Exception.ToString());
            Log.CloseAndFlush();

            Application.Exit();
        }

        /// <summary>
        /// Creates the DataDir folder in AppData/Local if it's not already present.
        /// </summary>
        static void CreateDataDirIfNotPresent()
        {
            if (!Directory.Exists(DataDir))
            {
                Directory.CreateDirectory(DataDir);
            }
        }

        /// <summary>
        /// Copies the database file from the deployment location to the DataDir folder if it's not already present.
        /// </summary>
        static void CopyDatabaseToDataDirIfNotPresent()
        {
            string sourceFilePath = Path.Combine(Application.StartupPath, Constants.DatabaseName);
            string destFilePath = Path.Combine(DataDir, Constants.DatabaseName);

            if (!File.Exists(destFilePath))
            {
                File.Copy(sourceFilePath, destFilePath);
            }
        }

        /// <summary>
        /// Creates builder, configures services, and initializes host.
        /// </summary>
        /// <returns>An initialized IHost.</returns>
        static IHost InitializeHost()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IFeedSleepRepeatLogic, FeedSleepRepeatLogic>();
                    services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
                    services.AddSingleton<FeedForm>();
                })
                .UseSerilog()
                .Build();

            return host;
        }

        /// <summary>
        /// Logs application closure then closes and flushes log.
        /// </summary>
        static void LogClosureAndFlush()
        {
            Log.Information(Constants.AppClosure);
            Log.CloseAndFlush();
        }
    }
}
