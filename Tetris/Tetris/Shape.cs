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
        //public abstract bool CheckLeftSide();
        //public abstract bool CheckRightSide();
        //public abstract bool CheckDownSide();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void RotLeft();
        public abstract void RotRight();

    }
}
