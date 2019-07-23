using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopSimulator2015
{
    /// <summary>
    /// Stellt Methoden und Eigenschaften zum erzeugen eines Captchas bereit.
    /// </summary>
    class CaptchaGenerator : IDisposable
    {
        protected Random rnd;
        private Bitmap _Captcha = null;

        protected const int spacePerChar = 25;//Abstand zwischen den Zeichen

        protected void DrawChar(int c, Graphics g)
        {
            int y = rnd.Next(8, 13);
            int fontSize = this.rnd.Next(12, 18);
            //move rotation point to center of image
            g.TranslateTransform(spacePerChar * (c) + 10, (30 - fontSize) / 2);
            g.RotateTransform(rnd.Next(this.RotateRange.Min, this.RotateRange.Max));
            g.DrawString(GetRandomChar(), GetRandomFont(fontSize), GetRandomFontBrush(), new PointF(5, y));
            g.ResetTransform();
        }

        protected Font GetRandomFont(int fontSize)
        {
            return new Font(new string[] { "Arial", "Consolas", "Verdena" }[this.rnd.Next(3)], fontSize);
        }
        protected string GetRandomChar()
        {
            string s = this.Chars[this.rnd.Next(this.Chars.Length)].ToString();
            this.Text += s;
            return s;
        }
        protected Brush GetRandomFontBrush()
        {
            int r = rnd.Next(0, 200);
            int g = rnd.Next(0, (200 - r) / 2);
            int b = 200 - r - g;
            if (b < 0)
                b = 0;
            return new SolidBrush(Color.FromArgb(r, g, b));
        }
        protected Brush GetRandomBackgroundHatchBrush()
        {
            return new HatchBrush((HatchStyle)this.rnd.Next(53), this.GetRandomBackgroundColor(), Color.Transparent);
        }
        protected Color GetRandomBackgroundColor()
        {
            int r = rnd.Next(180, 255);
            int g = rnd.Next(180, 255);
            int b = rnd.Next(180, 255);
            return Color.FromArgb(r, g, b);
        }
        protected Brush GetRandomForegroundHatchBrush()
        {
            return new HatchBrush((HatchStyle)this.rnd.Next(53), this.GetRandomForegroundColor(), this.GetRandomForegroundColor());
        }
        protected Color GetRandomForegroundColor()
        {
            int r = rnd.Next(180, 255);
            int g = rnd.Next(180, 255);
            int b = rnd.Next(180, 255);
            return Color.FromArgb(this.rnd.Next(10, 50), r, g, b);
        }

        #region .ctor

        private CaptchaGenerator()
        {
            this.RotateRange = new CaptchaCharRotateRange(-20, 20);
            this.IntegrateHatch = true;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der CaptchaGenerator-Klasse.
        /// </summary>
        /// <param name="length">Die Länge der Zeichenkette im Captcha.</param>
        /// <param name="chars">Die zu verwendenden Zeichen im Captcha.</param>
        public CaptchaGenerator(int length, string chars)
            : this()
        {
            this.rnd = new Random();
            this.Length = length;
            this.Chars = chars;
            this.Generate();
        }

        #endregion

        #region Methoden

        /// <summary>
        /// Erzeugt ein neues, zufälliges Captcha.
        /// </summary>
        public void Generate()
        {
            this.Text = string.Empty;

            //Größe des Bildes festlegen und dieses erzeugen
            int width = (this.Length + 1) * spacePerChar;
            int height = 60;
            _Captcha = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(_Captcha))
            {
                Rectangle rect = new Rectangle(0, 0, width, height);

                //Hintergundmalen
                g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(width, height), GetRandomBackgroundColor(), GetRandomBackgroundColor()), rect);
                if (this.IntegrateHatch)
                    g.FillRectangle(GetRandomBackgroundHatchBrush(), rect);
                //Zeichen malen
                for (int i = 0; i < this.Length; i++)
                {
                    DrawChar(i, g);
                }
                //Muster über den Text malen
                if (this.IntegrateHatch)
                    g.FillRectangle(GetRandomForegroundHatchBrush(), rect);

            }
        }

        #endregion

        #region Eigenschaften

        /// <summary>
        /// Ruft den angezeigten Text im Cpatcha ab.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// Ruft das Captcha ab.
        /// </summary>
        public Bitmap Captcha
        {
            get {
                return _Captcha;
            }
        }

        /// <summary>
        /// Ruft die Anzahl der Zeichen im Captcha ab bzw. legt diese fest.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Ruft die Verwendbaren Zeichen im Cpatcha ab oder legt diese fest.
        /// </summary>
        public string Chars { get; set; }

        /// <summary>
        /// Ruft den Drehbereich der einzelnen Zeichen ab oder legt diese fest. Der Idealwert liegt zwischen einschließlich -20 und 20.
        /// </summary>
        public CaptchaCharRotateRange RotateRange { get; set; }

        /// <summary>
        /// Ruft einen Wert ab, der angibt ob ein zufälliger Hatchbruch integriert werden soll oder legt diesen fest.
        /// </summary>
        public bool IntegrateHatch { get; set; }

        #endregion

        #region IDisposable Member

        /// <summary>
        /// Gibt alle von diesem Objekt verwendeten Resourcen wieder frei.
        /// </summary>
        public void Dispose()
        {
            if (this._Captcha != null)
                this._Captcha.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Stellt den Bereich dar, in dem ein Zeichen im Captcha gedrht werden kann.
    /// </summary>
    struct CaptchaCharRotateRange
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der CaptchaCharRotateRange-Klasse.
        /// </summary>
        /// <param name="min">Die höchste Drehung in Grad nach links.</param>
        /// <param name="max">Die höchste Drehung in Grad nach rechts.</param>
        public CaptchaCharRotateRange(int min, int max)
            : this()
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("min <= max!");
            this.Min = min;
            this.Max = max;
        }
        /// <summary>
        /// Ruft die maximale Drehung in Grad nach links ab.
        /// </summary>
        public int Min { get; private set; }
        /// <summary>
        /// Ruft die maximale Drehung in Grad nach rechts ab.
        /// </summary>
        public int Max { get; private set; }
    }
}
