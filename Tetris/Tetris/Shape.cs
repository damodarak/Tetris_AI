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
        public abstract bool CheckLeftSide();
        public abstract bool CheckRightSide();
        public abstract void RotateLeft();
        public abstract void RotateRight();

    }
}
