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
            Board = new char[18, 10];
        }
        public void AddToBoard(int[,] blocksLoc, char color)
        {
            for (int i = 0; i < 4; i++)
            {
                Board[blocksLoc[i, 0], blocksLoc[i, 1]] = color;
            }
        }
    }
}
