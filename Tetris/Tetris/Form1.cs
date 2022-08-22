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
        Image img;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    pictureBox1.Refresh();
                    activePiece.MoveLeft(ref gb);
                    Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
                    return true;
                case Keys.Up:
                    pictureBox1.Refresh();
                    activePiece.RotRight(ref gb);
                    Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
                    return true;
                case Keys.Right:
                    pictureBox1.Refresh();
                    activePiece.MoveRight(ref gb);
                    Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
                    return true;
                case Keys.Down:
                    timer1.Enabled = false;
                    pictureBox1.Refresh();
                    if (!activePiece.MoveDown(ref gb))
                    {
                        gb.AddToBoard(activePiece);
                        activePiece = new Ctverec();
                    }
                    Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
                    timer1.Enabled = true;
                    return true;
                case Keys.Z:
                    pictureBox1.Refresh();
                    activePiece.RotLeft(ref gb);
                    Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activePiece = new Ctverec();
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
            if(!activePiece.MoveDown(ref gb))
            {
                gb.AddToBoard(activePiece);
                activePiece = new Ctverec();
            }
            Visual.DrawGame(ref gb, activePiece, grafika, tuzka);
        }
    }
}
