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
            clearLines = new int[5];
        }
        Pen tuzka;
        Shape activePiece;
        Shape nextPiece;
        GameBoard gb;
        int[] clearLines;
        bool gameOver;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (activePiece==null)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            switch (keyData)
            {
                case Keys.W:
                    pictureBox1.Invalidate();
                    activePiece.MoveUp();
                    return true;
                case Keys.Space:
                    pictureBox1.Invalidate();
                    gb.score += activePiece.HardDrop(ref gb);
                    move(ref gb);
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
                    move(ref gb);
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
        private void updateInfo()
        {
            label4.Text = gb.level.ToString();
            label5.Text = gb.score.ToString();
            label6.Text = gb.lines.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearLines = new int[5];
            timer1.Enabled = false;
            activePiece = GameBoard.GeneratePiece();
            nextPiece = GameBoard.GeneratePiece();
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            gb = new GameBoard();
            updateInfo();
            timer1.Enabled = true;
            gameOver = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval == 200)
            {
                pictureBox1.Invalidate();
                pictureBox3.Invalidate();
                timer1.Enabled = false;
                GameBoard.MoveMap(ref gb, clearLines);
                timer1.Interval = 500;
                timer1.Enabled = true;
            }
            move(ref gb);
        }
        private void move(ref GameBoard gb)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            if (!activePiece.MoveDown(ref gb))
            {
                if (gb.AddToBoard(activePiece))
                {
                    clearLines = gb.FindFullLines(activePiece);
                    if (clearLines[4] != 0)
                    {
                        timer1.Enabled = false;
                        timer1.Interval = 200;
                        GameBoard.ClearLines(ref gb, clearLines);
                        timer1.Enabled = true;
                    }
                    activePiece = nextPiece;
                    nextPiece = GameBoard.GeneratePiece();
                    updateInfo();
                }
                else
                {
                    timer1.Enabled = false;
                    gameOver = true;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (activePiece==null)
            {
                return;
            }
            else if (gameOver)
            {
                Visual.DrawGame(ref gb, activePiece, e.Graphics, tuzka);
                e.Graphics.DrawImage(Properties.Resources.gover, 0, 150, 352, 200);
            }
            else
            {
                Visual.DrawGame(ref gb, activePiece, e.Graphics, tuzka);
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.tetris, 0, 0, 260, 90);
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            if (nextPiece==null)
            {
                return;
            }
            else
            {
                Visual.DrawNextPiece(nextPiece, e.Graphics, tuzka);
            }
        }
    }
}
