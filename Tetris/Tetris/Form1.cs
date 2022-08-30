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
        System.Media.SoundPlayer player;
        //keypress event,, faster moving down, hudba, sound effects, sideBlocking
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
        int moveSpeed;

        //ai
        int[,] placeToDropFrom;

        //for testing purpose
        static public int test1 = 0;
        static public int test2 = 0;
        static public int test3 = 0;
        static public int test4 = 0;
        static public int test5 = 0;

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
        private void updateInfo()//
        {
            //test
            label7.Text = test1.ToString();
            label8.Text = test2.ToString();
            label9.Text = test3.ToString();
            label10.Text = test4.ToString();
            label11.Text = test5.ToString();
            //

            label4.Text = gb.level.ToString();
            label5.Text = gb.score.ToString();
            label6.Text = gb.lines.ToString();          
            if (timer2.Enabled == false && gb.level <= 10 && timer1.Interval != 201)
            {
                moveSpeed = 500 - (gb.level - 1) * 40;
                timer1.Interval = moveSpeed;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval == 201)
            {
                pictureBox1.Invalidate();
                pictureBox3.Invalidate();
                timer1.Enabled = false;
                GameBoard.MoveMap(ref gb, clearLines);
                timer1.Interval = moveSpeed;
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
                        timer1.Interval = 201;
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
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            player = new System.Media.SoundPlayer(Properties.Resources.tetris_music);
            player.PlayLooping();
            placeToDropFrom = new int[5, 2];
            timer1.Enabled = false;
            timer2.Enabled = false;
            clearLines = new int[5];
            activePiece = GameBoard.GeneratePiece();
            nextPiece = GameBoard.GeneratePiece();
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            gb = new GameBoard();
            placeToDropFrom = HardDropAI.FindBestPlaceForDrop(ref gb, activePiece);
            updateInfo();
            gameOver = false;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();

            if (!HardDropAI.PlayNextMove(ref gb, activePiece, placeToDropFrom))
            {
                if (!gb.AddToBoard(activePiece))
                {
                    timer2.Enabled = false;
                    gameOver = true;
                    player.Stop();
                }
                gb.AddToBoard(activePiece);              
                clearLines = gb.FindFullLines(activePiece);
                GameBoard.ClearLines(ref gb, clearLines);
                GameBoard.MoveMap(ref gb, clearLines);
                activePiece = nextPiece;
                nextPiece = GameBoard.GeneratePiece();
                placeToDropFrom = HardDropAI.FindBestPlaceForDrop(ref gb, activePiece);
                updateInfo();
            }         
        }
    }
}
