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
        int vyska = 0;
        int sirka = 0;
        Graphics grafika;
        Pen tuzka;
        Tyc t;
        string[] barvy = { "Red", "Violet", "Yellow", "DBlue", "LBlue", "Green", "Orange" };

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Visual.DrawRect(grafika, tuzka, barvy[r.Next(0, 7)], i, j);
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    sirka -= 1;
                    pictureBox1.Refresh();
                    Visual.DrawRect(grafika, tuzka, "Violet", vyska, sirka);
                    return true;
                case Keys.Up:
                    vyska -= 1;
                    pictureBox1.Refresh();
                    Visual.DrawRect(grafika, tuzka, "Violet", vyska, sirka);
                    return true;
                case Keys.Right:
                    sirka += 1;
                    pictureBox1.Refresh();
                    Visual.DrawRect(grafika, tuzka, "Violet", vyska, sirka);
                    return true;
                case Keys.Down:
                    vyska += 1;
                    pictureBox1.Refresh();
                    Visual.DrawRect(grafika, tuzka, "Violet", vyska, sirka);
                    t.MoveDown();
                    Visual.DrawShape(t, grafika, tuzka);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t = new Tyc();
            Visual.DrawShape(t, grafika, tuzka);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
