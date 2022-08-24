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
            int level = (gb.lines / 10) + 1;
            label4.Text = level.ToString();
            label5.Text = gb.score.ToString();
            label6.Text = gb.lines.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activePiece = GameBoard.GeneratePiece();
            pictureBox1.Invalidate();
            gb = new GameBoard();
            updateInfo();
            timer1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            move(ref gb);
        }
        private void move(ref GameBoard gb)
        {
            pictureBox1.Invalidate();
            if (!activePiece.MoveDown(ref gb))
            {
                gb.AddToBoard(activePiece);
                gb.FindFullLines(activePiece);
                activePiece = GameBoard.GeneratePiece();
                updateInfo();
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

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.tetris, 0, 0, 260, 90);
        }
    }
}
