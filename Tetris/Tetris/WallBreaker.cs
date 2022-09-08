/*
Tetris
David Kroupa, I. ročník, 31. st. skupina
letní semestr 2021/22
Programování 2 NPRG031
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class WallBreaker
    {
        //change public to private
        public char[,] Board;
        public int Hrac;
        public int[] Strela;
        public bool reload;
        public int level;
        public int score;
        public bool gameover;
        public WallBreaker()
        {
            Board = new char[18, 10];
            Hrac = 4;
            Strela = new int[2] { -1, -1 };
            reload = false;
            level = 1;
            score = 0;
            gameover = false;
        }
        private char[] GenerateLine()
        {
            Random r = new Random();
            char[] konec = new char[10];
            for (int i = 0; i < 10; i++)
            {
                if (r.Next(1,10)<5)
                {
                    int cis = r.Next(0, 7);
                    switch (cis)
                    {
                        case 0:
                            konec[i] = 'R';
                            break;
                        case 1:
                            konec[i] = 'D';
                            break;
                        case 2:
                            konec[i] = 'L';
                            break;
                        case 3:
                            konec[i] = 'V';
                            break;
                        case 4:
                            konec[i] = 'Y';
                            break;
                        case 5:
                            konec[i] = 'O';
                            break;
                        case 6:
                            konec[i] = 'G';
                            break;
                    }
                }
                else
                {
                    konec[i] = '\0';
                }
            }
            return konec;
        }
        public bool MoveMap()
        {
            if (!HardDropAI.checkLineClear(ref this.Board, 16))
            {
                return false;
            }
            else
            {
                for (int j = 17; j > 0; j--)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Board[j, k] = Board[j - 1, k];
                    }
                }
                char[] prvni = GenerateLine();
                for (int i = 0; i < 10; i++)
                {
                    Board[0, i] = prvni[i];
                }
                return true;
            }
        }
        public void MoveLeft()
        {
            if (Hrac>0)
            {
                Hrac--;
            }
        }
        public void MoveRight()
        {
            if (Hrac<9)
            {
                Hrac++;
            }
        }
        public void Shoot()
        {
            if (!reload)
            {
                Strela[0] = 16;
                Strela[1] = Hrac;
                reload = true;
            }
        }
        public bool Hit()
        {
            if (Strela[0]<0)
            {
                reload = false;
                Strela[0] = -1;
                Strela[1] = -1;             
                return false;
            }
            return this.Board[Strela[0], Strela[1]] != '\0';
        }
        public void ProceedShot()
        {
            Strela[0]--;
        }
        public void DelHitBlock()
        {
            score++;
            level = (score / 20) + 1;
            reload = false;
            this.Board[Strela[0], Strela[1]] = '\0';
            Strela[0] = -1;
            Strela[1] = -1;
        }
        public int GameSpeed()
        {
            return 1000 - (level - 1) * 30;
        }
    }
}
