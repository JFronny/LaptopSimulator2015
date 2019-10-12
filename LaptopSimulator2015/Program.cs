using LaptopSimulator2015.Properties;
using System;
using System.Drawing;
using System.IO;
#if !DEBUG
using System.Runtime.InteropServices;
#endif
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LaptopSimulator2015
{
    class Program
    {
        public static Splash splash;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Settings.Load();
            Thread.CurrentThread.CurrentUICulture = Settings.lang;
            Console.Title = "LaptopSimulator2015";
            splash = new Splash();
            splash.Show();
            Thread.Sleep(2000);
#if !DEBUG
            FileStream filestream = new FileStream(".log", FileMode.Create);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
#endif
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine(strings.consoleStarting);
            if (Settings.level == -1)
                while (Settings.level == -1)
                    Application.Run(new Tutorial());
            Application.Run(new FakeDesktop());
            Console.WriteLine(strings.consoleQuit);
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
