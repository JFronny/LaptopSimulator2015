using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LaptopSimulator2015 {
    public static class Settings {
        public static readonly string _xmlfile = Path.GetDirectoryName(Application.ExecutablePath) + @"\save.xml";
        public static void Save()
        {
            XElement xmldoc_temp = new XElement("save");
            xmldoc_temp.Add(new XElement("wam", wam));
            xmldoc_temp.Add(new XElement("lsd", lsd));
            xmldoc_temp.Add(new XElement("subs", subs));
            xmldoc_temp.Add(new XElement("level", level));
            xmldoc_temp.Add(new XElement("lang", lang));
            xmldoc_temp.Save(_xmlfile);
        }
        public static void Load()
        {
            if (!File.Exists(_xmlfile))
            {
                XElement xmldoc_temp = new XElement("save");
                xmldoc_temp.Add(new XElement("wam", 10));
                xmldoc_temp.Add(new XElement("lsd", false));
                xmldoc_temp.Add(new XElement("subs", true));
                xmldoc_temp.Add(new XElement("level", 1));
                xmldoc_temp.Add(new XElement("lang", CultureInfo.CurrentCulture));
                xmldoc_temp.Save(_xmlfile);
            }
            XElement xmldoc = XElement.Load(_xmlfile);
            wam = int.Parse(xmldoc.Element("wam").Value);
            lsd = bool.Parse(xmldoc.Element("lsd").Value);
            subs = bool.Parse(xmldoc.Element("subs").Value);
            level = int.Parse(xmldoc.Element("level").Value);
            lang = CultureInfo.GetCultureInfo(xmldoc.Element("lang").Value);
        }
        public static int wam;
        public static bool lsd;
        public static bool subs;
        public static int level;
        public static CultureInfo lang;
    }
}