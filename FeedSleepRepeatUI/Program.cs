using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedSleepRepeatUI
{
    static class Program
    {
        static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static readonly string DataDir = Path.Combine(LocalAppData, "FSR");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateDataDirIfDoesNotExist();
            CopyDatabaseToDataDirIfDoesNotExist();
            AppDomain.CurrentDomain.SetData("DataDirectory", DataDir);
            Application.Run(new FeedForm());
        }

        /// <summary>
        /// Shows message box id fatal error occurs and exits application when user clicks OK.
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // TODO: Add exception logging

            MessageBox.Show(
                "Sorry for the disruption.\n\n"
                + "Unfortunately, FeedSleepRepeat has stopped working because the following fatal error occured:\n\n"
                + e.Exception.Message,
                "Fatal Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);

            Application.Exit();
        }

        /// <summary>
        /// Creates the DataDir folder in AppData/Local if it doesn't already exist.
        /// </summary>
        static void CreateDataDirIfDoesNotExist()
        {
            if (!Directory.Exists(DataDir))
            {
                Directory.CreateDirectory(DataDir);
            }
        }

        /// <summary>
        /// Copies the database file from the deployment location to the DataDir folder if it doesn't already exist.
        /// </summary>
        static void CopyDatabaseToDataDirIfDoesNotExist()
        {
            string sourceFilePath = Path.Combine(Application.StartupPath, "FeedSleepRepeatDB.db");
            string destFilePath = Path.Combine(DataDir, "FeedSleepRepeatDB.db");

            if (!File.Exists(destFilePath))
            {
                File.Copy(sourceFilePath, destFilePath);
            }
        }
    }
}
