using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    abstract class Shape
    {
        string nazev;
        int[] shape;
        public int[,] pozice;
        int rotNum;
        public abstract void MoveDown();
        public abstract void MoveLeft(ref GameBoard gb);
        public abstract void MoveRight(ref GameBoard gb);
        public abstract void RotLeft();
        public abstract void RotRight();

    }
}
