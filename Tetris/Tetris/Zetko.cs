﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Zetko : Shape
    {
        public char Color = 'G';
        int rotNum;
        public Zetko()
        {
            Pozice = new int[4, 2] { { 2, 3 }, { 2, 4, }, { 3, 4 }, { 3, 5 } };
            rotNum = 0;
        }
        private bool checkRotZero(ref GameBoard gb)
        {
            return (gb.Board[Pozice[0, 0] - 1, Pozice[0, 1] + 2] == '\0' &&
                gb.Board[Pozice[3, 0] - 1, Pozice[3, 1]] == '\0');

        }
        private bool checkRotOne(ref GameBoard gb)
        {
            return (Pozice[1, 1] != 0 && gb.Board[Pozice[0, 0] + 1, Pozice[0, 1] - 2] == '\0' &&
                gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == '\0');
        }
        public override void MoveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                Pozice[i, 0] -= 1;
            }
        }
        public override bool MoveDown(ref GameBoard gb)
        {
            if (checkDownSide(ref gb))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 0] += 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool MoveLeft(ref GameBoard gb)
        {
            if (checkLeftSide(ref gb))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] -= 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool MoveRight(ref GameBoard gb)
        {
            if (checkRightSide(ref gb))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] += 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool RotRight(ref GameBoard gb)
        {
            if (rotNum==0 && checkRotZero(ref gb))
            {
                Pozice[0, 0] -= 1;
                Pozice[0, 1] += 2;
                Pozice[3, 0] -= 1;
                rotNum = (++rotNum) % 2;
                return true;
            }
            else if (rotNum==1 && checkRotOne(ref gb))
            {
                Pozice[0, 0] += 1;
                Pozice[0, 1] -= 2;
                Pozice[3, 0] += 1;
                rotNum = (++rotNum) % 2;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void RotLeft(ref GameBoard gb)
        {
            RotRight(ref gb);
        }
        public override int HardDrop(ref GameBoard gb)
        {
            int pocet = 0;
            while (MoveDown(ref gb))
            {
                ++pocet;
            }
            return pocet;
        }
    }
}
