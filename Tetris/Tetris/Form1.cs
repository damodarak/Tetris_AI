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
            tuzka = new Pen(Color.Black, 2);//barva, sirka
        }
        Pen tuzka;
        Shape activePiece;
        GameBoard gb;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (activePiece==null)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            switch (keyData)
            {
                case Keys.Space:
                    return true;
                case Keys.Left:
                    pictureBox1.Invalidate();
                    activePiece.MoveLeft(ref gb);
                    return true;
                case Keys.Up:
                    pictureBox1.Invalidate();
                    activePiece.RotRight(ref gb);
                    return true;
                case Keys.Right:
                    pictureBox1.Invalidate();
                    activePiece.MoveRight(ref gb);
                    return true;
                case Keys.Down:
                    timer1.Enabled = false;
                    pictureBox1.Invalidate();
                    if (!activePiece.MoveDown(ref gb))
                    {
                        gb.AddToBoard(activePiece);
                        activePiece = GameBoard.GeneratePiece();
                    }
                    timer1.Enabled = true;
                    return true;
                case Keys.Z:
                    pictureBox1.Invalidate();
                    activePiece.RotLeft(ref gb);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activePiece = GameBoard.GeneratePiece();
            pictureBox1.Invalidate();
            gb = new GameBoard();
            timer1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            if(!activePiece.MoveDown(ref gb))
            {
                gb.AddToBoard(activePiece);
                activePiece = GameBoard.GeneratePiece();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (activePiece==null)
            {
                return;
            }
            else
            {
                Visual.DrawGame(ref gb, activePiece, e.Graphics, tuzka);
            }
        }
    }
}
