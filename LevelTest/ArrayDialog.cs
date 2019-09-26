using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelTest
{
    public partial class ArrayDialog : Form
    {
        public int returnIndex;
        public ArrayDialog(string[] items)
        {
            InitializeComponent();
            listBox1.Items.AddRange(items);
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            returnIndex = listBox1.SelectedIndex;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ArrayDialog_Load(object sender, EventArgs e) => BringToFront();
    }
}
