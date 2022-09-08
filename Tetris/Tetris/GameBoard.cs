/*
Tetris
David Kroupa, I. ročník, 31 st. skupina
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
    class GameBoard
    {
        //colors - Orange, Red, Violet, Yellow, Green, Darkblue, Lightblue
        static int numOfPieces = 0;
        static bool[] piecesDistribution = new bool[7];//informace o tom, ktere TetroBlocky jsme uz v danem cyklu pouzili
        static Random r = new Random(Environment.TickCount);

        public char[,] Board;
        public int lines;
        public int level;
        public int score;
        private int[] points;

        public GameBoard()
        {
            Board = new char[20, 10];
            lines = 0;
            level = 1;
            score = 0;
            points = new int[5] { 0, 40, 100, 300, 1200 };
        }
        public bool AddToBoard(Shape shp)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Board[shp.Pozice[i, 0], shp.Pozice[i, 1]] == '\0')
                {
                    Board[shp.Pozice[i, 0], shp.Pozice[i, 1]] = shp.Color;//zapisujeme do hraci desky barvu TetroBlocku
                }
                else
                {
                    return false;//na danem miste jiz neco je, tim padem nemuzeme vlozit figurku do hraci desky
                }
            }
            return true;
        }
        public GameBoard Copy()
        {
            GameBoard gbnew = new GameBoard();
            gbnew.lines = this.lines;
            gbnew.level = this.level;
            gbnew.score = this.score;
            gbnew.Board = (char[,])this.Board.Clone();
            return gbnew;
        }
        static public Shape GeneratePiece()
        {
            ++numOfPieces;
            int cis;

            if (numOfPieces == 8)//ukonceny cyklus, zacatek noveho
            {
                piecesDistribution = new bool[7];
                numOfPieces = 1;
                cis = r.Next(0, 7);
            }
            else if(numOfPieces == 7)
            {
                cis = 0;
                while (piecesDistribution[cis])
                {
                    ++cis;
                }
            }
            else
            {
                cis = r.Next(0, 7);
            }


            while (piecesDistribution[cis])//dokud nenajdeme jeste nepouzity tvar v tomto cyklu
            {
                cis = r.Next(0, 7);
            }

            
            switch (cis)
            {
                case 0:
                    piecesDistribution[cis] = true;
                    return new Ctverec();
                case 1:
                    piecesDistribution[cis] = true;
                    return new Elko();
                case 2:
                    piecesDistribution[cis] = true;
                    return new Esko();
                case 3:
                    piecesDistribution[cis] = true;
                    return new Jecko();
                case 4:
                    piecesDistribution[cis] = true;
                    return new Tecko();
                case 5:
                    piecesDistribution[cis] = true;
                    return new Tyc();
                case 6:
                    piecesDistribution[cis] = true;
                    return new Zetko();
                default:
                    return new Tyc();
            }
        }
        public int[] FindFullLines()
        {
            int[] konec = new int[5];
            int j;
            for (int i = 2; i < 20; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    if (this.Board[i,j] == '\0')//rada neni naplnena bloky
                    {
                        break;
                    }
                }
                if (j==10)//je naplnena bloky a cislo rady ulozime do pole konec
                {
                    konec[konec[4]] = i;
                    ++konec[4];//pocet nalezenych rad        
                }
            }
            updateInfo(konec[4]);
            return konec;
        }
        static public int[] FindFullLines(ref char[,] deska)
        {
            //stejna logika jako v metode FindFullLines()
            int[] konec = new int[5];
            int j;
            for (int i = 2; i < 20; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    if (deska[i, j] == '\0')
                    {
                        break;
                    }
                }
                if (j == 10)
                {
                    konec[konec[4]] = i;
                    ++konec[4];
                }
            }
            return konec;
        }
        static public bool contains(int[] kde, int co)
        {
            //zjistime, zda prvek co se nachazi v poli kde
            for (int i = 0; i < 4; i++)
            {
                if (kde[i] == co)
                {
                    return true;
                }
            }
            return false;
        }
        private void updateInfo(int numLines)
        {
            score += points[numLines] * level;//system score
            lines += numLines;
            level = (lines / 10) + 1;           
        }
        static public void MoveMap(ref char[,] deska, int[] lines)
        {
            for (int i = 0; i < lines[4]; i++)//lines[4] je pocet rad, ktere byly vymazany
            {
                for (int j = lines[i]; j > 0; j--)//posuneme vsechno dolu i rady ktere nahore nevidime, a tim se uvolni misto
                {
                    for (int k = 0; k < 10; k++)//vsechny sloupce v jednom radku
                    {
                        deska[j, k] = deska[j - 1, k];//ctverec o jedna vys jde niz
                    }
                }
            }
        }
        static public void ClearLines(ref GameBoard gb, int[] lines)
        {
            for (int i = 0; i < lines[4]; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    gb.Board[lines[i], j] = '\0';
                }
            }
        }
        static public void markPozice(ref char[,] deska, int[,] Pozice)
        {
            //oznackovani pozice
            for (int i = 0; i < 4; i++)
            {
                deska[Pozice[i, 0], Pozice[i, 1]] = 'P';//Pozice
            }
        }
    }
}
