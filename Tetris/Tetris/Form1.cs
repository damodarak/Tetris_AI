using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics grafika = pictureBox1.CreateGraphics(); //vytvoreni grafiky
            Pen tuzka = new Pen(Color.Aqua, 5);//barva, sirka
            grafika.DrawLine(tuzka, 0, 0, 350, 630);//cim, odkud kam
        }
    }
}
