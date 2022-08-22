using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    abstract class Shape
    {
        private string nazev;
        public int[,] Pozice;
        private int rotNum;
        public abstract bool MoveDown(ref GameBoard gb);
        public abstract void MoveLeft(ref GameBoard gb);
        public abstract void MoveRight(ref GameBoard gb);
        public abstract void RotLeft(ref GameBoard gb);
        public abstract void RotRight(ref GameBoard gb);

    }
}
