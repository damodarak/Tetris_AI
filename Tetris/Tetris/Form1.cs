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
        Shape activePiece;
        GameBoard gb;
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
                    pictureBox1.Refresh();
                    activePiece.MoveLeft(ref gb);
                    Visual.DrawShape(activePiece, grafika, tuzka);
                    return true;
                case Keys.Up:
                    pictureBox1.Refresh();
                    activePiece.RotRight();
                    Visual.DrawShape(activePiece, grafika, tuzka);
                    return true;
                case Keys.Right:
                    pictureBox1.Refresh();
                    activePiece.MoveRight(ref gb);
                    Visual.DrawShape(activePiece, grafika, tuzka);
                    return true;
                case Keys.Down:
                    timer1.Enabled = false;
                    pictureBox1.Refresh();
                    activePiece.MoveDown();
                    Visual.DrawShape(activePiece, grafika, tuzka);
                    timer1.Enabled = true;
                    return true;
                case Keys.Z:
                    pictureBox1.Refresh();
                    activePiece.RotLeft();
                    Visual.DrawShape(activePiece, grafika, tuzka);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //t = new Tyc();
            activePiece = new Tyc();
            Visual.DrawShape(activePiece, grafika, tuzka);
            gb = new GameBoard();
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            activePiece.MoveDown();
            Visual.DrawShape(activePiece, grafika, tuzka);
        }
    }
}
