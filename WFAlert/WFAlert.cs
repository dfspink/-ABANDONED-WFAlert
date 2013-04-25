using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using LinqToTwitter;

using System.Collections.Generic; // debug
using System.Diagnostics;   // debug

namespace WFAlert
{
    static class WFAlert
    {
        public static TwitterManager TM = new TwitterManager();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TM.Run();
            MainGUI GUI = new MainGUI();
            Application.Run(GUI);
        }
    }
}