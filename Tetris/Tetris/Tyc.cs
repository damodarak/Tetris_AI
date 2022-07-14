using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tyc : Shape
    {
        string nazev = "Tyc";
        public string color = "Orange";
        public int[,] pozice;
        int rotNum = 0;
        public Tyc()
        {
            pozice = new int[4, 2] { { 0, 3, }, { 0, 4 }, { 0, 5 }, { 0, 6 } };
        }
        public override bool CheckDownSide()
        {
            if (rotNum == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pozice[i, 0] + 1 > 17)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void MoveDown()
        {
            for (int i = 0; i < 4; i++)
            {
                pozice[i, 0] += 1;
            }
        }
    }
}
