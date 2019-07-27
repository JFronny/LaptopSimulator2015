using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LaptopSimulator2015
{
    class Program
    {
        static void Main(string[] args)
        {
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.Title = "LaptopSimulator2015";
#if DEBUG
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
            Application.Run(new FakeDesktop());
            Console.WriteLine(strings.consoleQuit);
            Thread.Sleep(1000);
            Console.Clear();
        }
#if !DEBUG
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            try { Application.OpenForms[i].Close(); } catch {  }
            SetForegroundWindow(GetConsoleWindow());
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(strings.consoleError + "\r\n");
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("\r\n" + strings.consolePress);
            Thread.Sleep(1000);
            Console.ReadKey();
            Console.Clear();
            Environment.Exit(1);
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
#endif
    }
}
