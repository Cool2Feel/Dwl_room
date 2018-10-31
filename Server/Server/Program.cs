using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRuned;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out isRuned);
            if (isRuned)
            {
                try
                {
                    if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ServerForm());
                    mutex.ReleaseMutex();
                }
                catch (Exception e)
                {
                    //Console.WriteLine("message =" + e.Message);
                    ExceptionLog.getLog().WriteLogFile(e, "LogFile.txt");
                }
            }
            else
            {
                MessageBox.Show("        程序已启动！\r\n(Program has started)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
