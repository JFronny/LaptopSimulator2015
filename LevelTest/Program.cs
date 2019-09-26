using LaptopSimulator2015;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args != null && args.Length > 0)
                {
                    if (Directory.Exists(args[0].Replace("\"", "")))
                        Directory.SetCurrentDirectory(args[0].Replace("\"", ""));
                    else if (File.Exists(args[0].Replace("\"", "")))
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(args[0].Replace("\"", "")));
                    else
                        throw new Exception("Invalid argument");
                }
                else
                {
                    string[] tmp = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories).Where(s => Path.GetFileName(s) != "Base.dll").ToArray();
                    if (tmp.Length == 0)
                        using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
                        {
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                                Directory.SetCurrentDirectory(openFileDialog.SelectedPath);
                            else
                                throw new Exception("Please select a folder");
                        }
                }
                Minigame[] levels = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories).Where(s => Path.GetFileName(s) != "Base.dll")
                    .Select(s => Assembly.LoadFrom(s)).SelectMany(s => s.GetTypes()).Distinct()
                    .Where(p => typeof(Minigame).IsAssignableFrom(p)).Distinct().Except(new Type[] { typeof(Minigame), typeof(Level), typeof(Goal) })
                    .Select(s => (Minigame)Activator.CreateInstance(s)).OrderBy(lv => lv.availableAfter).ToArray();
                Minigame level;
                if (levels.Length == 0)
                    throw new Exception("No Levels found!");
                else if (levels.Length == 1)
                    level = levels[0];
                else
                {
                    using (ArrayDialog dialog = new ArrayDialog(levels.Select(s => s.name).ToArray(), "Select a Minigame"))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                            level = levels[dialog.returnIndex];
                        else
                            throw new Exception("Please select a folder");
                    }
                }
                Application.Run(new MainForm(level));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
