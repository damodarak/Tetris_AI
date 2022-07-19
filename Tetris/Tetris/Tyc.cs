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
        private  bool CheckDownSide()
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
                if (pozice[3,0] + 1 > 17)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public override void MoveDown()
        {
            if (CheckDownSide())
            {
                for (int i = 0; i < 4; i++)
                {
                    pozice[i, 0] += 1;
                }
            }
        }
        public override void MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                pozice[i, 1] -= 1;
            }
        }
        public override void MoveRight()
        {
            for (int i = 0; i < 4; i++)
            {
                pozice[i, 1] += 1;
            }
        }
        public override void RotRight()
        {
            if (rotNum == 0)
            {
                rotNum = 1;

                pozice[0, 0] -= 1;
                pozice[0, 1] += 1;

                pozice[2, 0] += 1;
                pozice[2, 1] -= 1;

                pozice[3, 0] += 2;
                pozice[3, 1] -= 2;
            }
            else
            {
                rotNum = 0;

                pozice[0, 0] += 1;
                pozice[0, 1] -= 1;

                pozice[2, 0] -= 1;
                pozice[2, 1] += 1;

                pozice[3, 0] -= 2;
                pozice[3, 1] += 2;
            }
        }
        public override void RotLeft()
        {
            RotRight();
        }
    }
}
