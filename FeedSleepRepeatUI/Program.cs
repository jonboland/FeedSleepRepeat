using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FeedSleepRepeatLibrary;

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
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.SetData("DataDirectory", DataDir);
            CreateDataDirIfNotPresent();
            CopyDatabaseToDataDirIfNotPresent();           
            Application.Run(new FeedForm());
        }

        /// <summary>
        /// Shows message box if fatal error occurs and exits application when user clicks OK.
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // TODO: Add exception logging

            MessageBox.Show(
                Constants.FatalErrorOccured + e.Exception.Message,
                Constants.FatalErrorCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);

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
    }
}
