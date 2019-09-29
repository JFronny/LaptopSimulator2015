using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LaptopSimulator2015
{
    public class WizardTab : TabControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var filteredKeys = new Keys[]{(Keys.Control | Keys.Tab),
            (Keys.Control | Keys.Shift | Keys.Tab),
            Keys.Left, Keys.Right, Keys.Home, Keys.End};
            if (filteredKeys.Contains(keyData))
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public const int TCM_FIRST = 0x1300;
        public const int TCM_ADJUSTRECT = TCM_FIRST + 40;
        public WizardTab()
        {
            Appearance = TabAppearance.Buttons;
            ItemSize = new Size(1, 0);
            SizeMode = TabSizeMode.Fixed;
            TabStop = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == TCM_ADJUSTRECT && !DesignMode)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }
    }
}
