using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using toyota.Class;

namespace TOYOTA_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static double Width = 1366;
        public static double Height = 768;
        public static Config config;
        public static CacheUserPin pin;
        public static string fileName = "config.xml";
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int len = e.Args.Length;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (!string.IsNullOrEmpty(e.Args[i]))
                    {
                        fileName = e.Args[i];
                    }
                }
            }
            pin = CacheUserPin.Read("userPin.xml");
            config = Config.Read(fileName);
            if (!Directory.Exists(config.folderSave))
            {
                Directory.CreateDirectory(config.folderSave);
            }
            if (!Directory.Exists(config.folderData))
            {
                Directory.CreateDirectory(config.folderData);
            }
            if (!Directory.Exists(config.folderCache))
            {
                Directory.CreateDirectory(config.folderCache);
            }
            if (!string.IsNullOrEmpty(config.runFile))
            {
                FileInfo fInfo = new FileInfo(config.runFile);
                ProcessStartInfo pInfo = new ProcessStartInfo(fInfo.Name);
                pInfo.WorkingDirectory = fInfo.DirectoryName;
                pInfo.Arguments = "config_clone.xml";
                bool isStart = true;
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "Folder Clone")
                    {
                        isStart = false;
                    }
                }
                // if(pInfo.is)
                if (isStart)
                {
                    Process p = Process.Start(pInfo);
                }
            }
            if (!string.IsNullOrEmpty(config.PrintManager))
            {
                FileInfo fInfo = new FileInfo(config.PrintManager);
                ProcessStartInfo pInfo = new ProcessStartInfo(fInfo.Name);
                pInfo.WorkingDirectory = fInfo.DirectoryName;
                bool isStart = true;
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "TOYOTA CMD")
                    {
                        isStart = false;
                    }
                }
                if (isStart)
                {
                    Process p = Process.Start(pInfo);
                }
            }
            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
        }
    }
}
