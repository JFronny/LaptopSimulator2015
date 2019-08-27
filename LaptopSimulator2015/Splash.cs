using LaptopSimulator2015.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaptopSimulator2015
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            switch (new Random().Next(5))
            {
                case 0:
                    BackgroundImage = Resources.Cryengine;
                    break;
                case 1:
                    BackgroundImage = Resources.GameMaker;
                    break;
                case 2:
                    BackgroundImage = Resources.Godot;
                    break;
                case 3:
                    BackgroundImage = Resources.Unity;
                    break;
                default:
                    BackgroundImage = Resources.Unreal;
                    break;
            }
        }
    }
}
