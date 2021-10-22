using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedSleepRepeatUI
{
    static class Program
    {
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
            Application.Run(new FeedForm());
        }

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
    }
}
