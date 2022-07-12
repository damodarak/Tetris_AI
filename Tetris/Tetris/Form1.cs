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
            grafika = pictureBox1.CreateGraphics(); //vytvoreni grafiky
            tuzka = new Pen(Color.Black, 2);//barva, sirka
        }
        Graphics grafika;
        Pen tuzka;
        string[] barvy = { "Red", "Violet", "Yellow", "DBlue", "LBlue", "Green", "Orange" };

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    Visual.DrawRect(grafika, tuzka, barvy[r.Next(0, 7)], i, j);
                }
            }
        }
    }
}
