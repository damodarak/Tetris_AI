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
        static int[] piecesDistribution = new int[7];
        public char[,] Board;
        public GameBoard()
        {
            Board = new char[20, 10];
        }
        public void AddToBoard(Shape shp)
        {
            dynamic tvar = shp;
            for (int i = 0; i < 4; i++)
            {
                Board[tvar.Pozice[i, 0], tvar.Pozice[i, 1]] = tvar.Color;
            }
        }
        static public Shape GeneratePiece()
        {
            Random r = new Random(Environment.TickCount);
            int cis;
            ++numOfPieces;
            cis = r.Next(0, 7);
            /*
             * in case of a bad luck
             * fair distribution and making 'rare' pieces fall more
             */
            if (numOfPieces % 5 == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (piecesDistribution[i]<piecesDistribution[cis])
                    {
                        cis = i;
                    }
                }
            }
            
            switch (cis)
            {
                case 0:
                    ++ piecesDistribution[cis];
                    return new Ctverec();
                case 1:
                    ++piecesDistribution[cis];
                    return new Elko();
                case 2:
                    ++piecesDistribution[cis];
                    return new Esko();
                case 3:
                    ++piecesDistribution[cis];
                    return new Jecko();
                case 4:
                    ++piecesDistribution[cis];
                    return new Tecko();
                case 5:
                    ++piecesDistribution[cis];
                    return new Tyc();
                case 6:
                    ++piecesDistribution[cis];
                    return new Zetko();
                default:
                    ++piecesDistribution[cis];
                    return new Tyc();
            }
        }
    }
}
