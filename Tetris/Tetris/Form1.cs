/*
Tetris
David Kroupa, I. ročník, 31 st. skupina
letní semestr 2021/22
Programování 2 NPRG031
*/
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
            player = new System.Media.SoundPlayer(Properties.Resources.tetris_music);
            playing = false;
            wb = false;
        }

        //hudba
        System.Media.SoundPlayer player;
        bool playing;

        //zakladni promenne pro chod hry ve vsech rezimech
        Pen tuzka;
        Shape activePiece;
        Shape nextPiece;
        GameBoard gb;
        int[] clearLines;
        bool gameOver;
        int moveSpeed;     

        //HardDropAI
        int[,] placeToDropFrom;

        //ImprovedAI
        string nav;//navigace od startu do chtene pozice
        int stepNum;//kolikaty krok se chystame udelat

        //ghost for AI
        static public int[,] Ghost;//pole, ktere je vykresleno a kam miri TetroBlock pomoc AI

        //WallBreaker
        bool wb;
        WallBreaker wbgame;

        //prepsani virtualni funkce ProcessCmdKey, protoze KeyDown Event naprosto a nijak nefunguje
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (wb && !wbgame.gameover)
            {
                switch (keyData)
                {
                    case Keys.Left:
                        pictureBox1.Invalidate();
                        wbgame.MoveLeft();
                        return true;
                    case Keys.Right:
                        pictureBox1.Invalidate();
                        wbgame.MoveRight();
                        return true;
                    case Keys.Space:
                        pictureBox1.Invalidate();
                        wbgame.Shoot();
                        timer5.Enabled = true;
                        return true;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else if (activePiece==null || timer2.Enabled || timer3.Enabled)
            {
                return base.ProcessCmdKey(ref msg, keyData);//nezapli jsme jeste hru, nebo bezi AI rezim
            }
            switch (keyData)
            {
                case Keys.Q://skryta funkce
                    pictureBox1.Invalidate();
                    activePiece.MoveUp();
                    return true;
                case Keys.Space:
                    timer1.Enabled = false;
                    pictureBox1.Invalidate();
                    gb.score += activePiece.HardDrop(ref gb);//pripsani bodu a zaroven zhozeni figurky na same dno
                    move(ref gb);
                    timer1.Enabled = true;
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
            //aktualize dat
            label4.Text = gb.level.ToString();
            label5.Text = gb.score.ToString();
            label6.Text = gb.lines.ToString();          
            if (!timer2.Enabled && !timer3.Enabled && gb.level <= 10 && timer1.Interval != 201)//pri zvysovani levelu se snizuje rychlost hry
            {
                moveSpeed = 500 - (gb.level - 1) * 40;
                timer1.Interval = moveSpeed;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //user tetris game tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            checkMusicPlayer();

            if (timer1.Interval == 201)
            {
                pictureBox1.Invalidate();
                pictureBox3.Invalidate();
                timer1.Enabled = false;
                GameBoard.MoveMap(ref gb.Board, clearLines);
                timer1.Interval = moveSpeed;
                timer1.Enabled = true;
            }
            move(ref gb);
        }
        //metoda pro pohyb pri uzivatelskem rezimu
        private void move(ref GameBoard gb)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            if (!activePiece.MoveDown(ref gb))
            {
                if (gb.AddToBoard(activePiece))
                {
                    clearLines = gb.FindFullLines();
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
                    player.Stop();
                    playing = false;
                }
            }
        }
        //main gameboard picturebox
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (activePiece==null && !wb)
            {
                return;
            }
            else if (wb)
            {
                Visual.DrawWB(ref wbgame, e.Graphics, tuzka);
                if (wbgame.gameover)
                {
                    e.Graphics.DrawImage(Properties.Resources.gover, 0, 150, 352, 200);
                }
            }
            else if (gameOver)
            {
                Visual.DrawGame(ref gb, activePiece, e.Graphics, tuzka);
                e.Graphics.DrawImage(Properties.Resources.gover, 0, 150, 352, 200);
            }
            else
            {
                if ((timer2.Enabled || timer3.Enabled) && Ghost != null)
                {
                    Visual.DrawGhost(ref gb, Ghost, e.Graphics, tuzka);
                }
                Visual.DrawGame(ref gb, activePiece, e.Graphics, tuzka);                              
            }
        }
        //tetris picture picturebox
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.tetris, 0, 0, 260, 90);
        }
        //nextPiece picturebox
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
            wb = false;
            timer4.Enabled = timer5.Enabled = false;

            if (checkBox1.Checked)
            {
                playing = true;
                player.PlayLooping();
            }

            timer3.Enabled = false;
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
        //harddrop AI button
        private void button3_Click(object sender, EventArgs e)
        {
            wb = false;
            timer4.Enabled = timer5.Enabled = false;

            if (checkBox1.Checked)
            {
                playing = true;
                player.PlayLooping();
            }
            
            placeToDropFrom = new int[5, 2];
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
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
        //harddrop AI tick
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();

            checkMusicPlayer();

            if (!HardDropAI.PlayNextMove(ref gb, activePiece, placeToDropFrom))
            {
                if (!gb.AddToBoard(activePiece))
                {
                    timer2.Enabled = false;
                    gameOver = true;
                    player.Stop();
                }
                else
                {
                    clearLines = gb.FindFullLines();
                    GameBoard.ClearLines(ref gb, clearLines);
                    GameBoard.MoveMap(ref gb.Board, clearLines);
                    activePiece = nextPiece;
                    nextPiece = GameBoard.GeneratePiece();
                    placeToDropFrom = HardDropAI.FindBestPlaceForDrop(ref gb, activePiece);
                    updateInfo();
                }             
            }         
        }
        //improved AI button
        private void button4_Click(object sender, EventArgs e)
        {
            wb = false;
            timer4.Enabled = timer5.Enabled = false;

            if (checkBox1.Checked)
            {
                playing = true;
                player.PlayLooping();
            }

            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            clearLines = new int[5];
            activePiece = GameBoard.GeneratePiece();
            nextPiece = GameBoard.GeneratePiece();
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            gb = new GameBoard();

            nav = AI.findBestPosition(ref gb, activePiece, nextPiece);
            stepNum = 0;

            updateInfo();
            gameOver = false;
            timer3.Enabled = true;
        }
        //improved AI tick
        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox3.Invalidate();

            checkMusicPlayer();

            if (!AI.PlayNextMove(ref gb, activePiece, nav, stepNum))//neni mozny pohyb dolu
            {
                if (!gb.AddToBoard(activePiece))//na miste figurky v desce uz neco je ulozeno --> konec hry
                {
                    timer3.Enabled = false;
                    gameOver = true;
                    player.Stop();
                }
                else
                {
                    clearLines = gb.FindFullLines();
                    GameBoard.ClearLines(ref gb, clearLines);
                    GameBoard.MoveMap(ref gb.Board, clearLines);
                    activePiece = nextPiece;
                    nextPiece = GameBoard.GeneratePiece();
                    nav = AI.findBestPosition(ref gb, activePiece, nextPiece);
                    stepNum = 0;
                    updateInfo();
                }                
            }
            else
            {
                ++stepNum;
            }
        }
        private void checkMusicPlayer()
        {
            if (checkBox1.Checked != playing)
            {
                playing = checkBox1.Checked;
                if (playing)
                {
                    player.PlayLooping();
                }
                else
                {
                    player.Stop();
                }
            }
        }
        //wall breaker play button
        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = timer2.Enabled = timer3.Enabled = false;
            nextPiece = null;
            activePiece = null;

            pictureBox1.Invalidate();
            pictureBox3.Invalidate();

            playing = false;
            player.Stop();
            wb = true;
            wbgame = new WallBreaker();
            label6.Text = "NA";
            updateWBScore();
            timer4.Enabled = true;
        }
        private void updateWBScore()
        {
            label4.Text = wbgame.level.ToString();
            label5.Text = wbgame.score.ToString();
            timer4.Interval = wbgame.GameSpeed();
        }
        //wall breaker tick
        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();

            if (wbgame.Hit())
            {
                wbgame.DelHitBlock();
            }
            else if(!wbgame.MoveMap())
            {
                timer4.Enabled = false;
                wbgame.gameover = true;
            }

            updateWBScore();
        }
        //wall breaker shooting tick
        private void timer5_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();

            if (!wbgame.reload)
            {
                timer5.Enabled = false;
            }
            else if (wbgame.Hit())
            {
                wbgame.DelHitBlock();
                timer5.Enabled = false;
                wbgame.reload = false;
            }
            else
            {
                wbgame.ProceedShot();
            }

            updateWBScore();
        }
    }
}
