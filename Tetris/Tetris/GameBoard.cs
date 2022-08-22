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
    }
}
