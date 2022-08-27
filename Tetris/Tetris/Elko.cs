using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Elko : Shape
    {
        public char Color = 'D';
        public int[,] Pozice;
        private int[,] rotationHack;
        private int[] stred;
        private int rotNum;
        private int rotHackNum;
        public Elko()
        {
            Pozice = new int[4, 2] { { 2, 3 }, { 2, 4 }, { 2, 5 }, { 3, 3 } };
            rotationHack = new int[4, 2] { { 0, -2 }, { -2, 0 }, { 0, 2 }, { 2, 0 } };
            stred = new int[2] { 2, 4 };
            rotNum = 1;
            rotHackNum = 1;
        }
        private bool checkRotRight(ref GameBoard gb)
        {
            return (stred[0] != 19 && stred[1] != 0 && stred[1] != 9 &&
                gb.Board[Pozice[0, 0] + (rotNum * -1), Pozice[0, 1] + (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[2, 0] - (rotNum * -1), Pozice[2, 1] - (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[3, 0] + rotationHack[rotHackNum, 0], Pozice[3, 1] + rotationHack[rotHackNum, 1]] == '\0');
        }
        private bool checkRotLeft(ref GameBoard gb)
        {
            return (stred[0] != 19 && stred[1] != 0 && stred[1] != 9 &&
                gb.Board[Pozice[0, 0] + (rotNum * -1), Pozice[0, 1] + (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[2, 0] - (rotNum * -1), Pozice[2, 1] - (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[3, 0] - rotationHack[(rotHackNum + 3) % 4, 0], Pozice[3, 1] - rotationHack[(rotHackNum + 3) % 4, 1]] == '\0');
        }
        public override void MoveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                Pozice[i, 0] -= 1;
            }
            stred[0] -= 1;
        }
        public override bool MoveDown(ref GameBoard gb)
        {
            if (checkDownSide(ref gb, Pozice))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 0] += 1;
                }
                stred[0] += 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void MoveLeft(ref GameBoard gb)
        {
            if (checkLeftSide(ref gb, Pozice))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] -= 1;
                }
                stred[1] -= 1;
            }
        }
        public override void MoveRight(ref GameBoard gb)
        {
            if (checkRightSide(ref gb, Pozice))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] += 1;
                }
                stred[1] += 1;
            }
        }
        public override void RotRight(ref GameBoard gb)
        {
            if (checkRotRight(ref gb))
            {
                Pozice[0, 0] += rotNum * -1;
                Pozice[0, 1] += rotNum * 1;
                Pozice[2, 0] -= rotNum * -1;
                Pozice[2, 1] -= rotNum * 1;
                Pozice[3, 0] += rotationHack[rotHackNum, 0];
                Pozice[3, 1] += rotationHack[rotHackNum, 1];
                rotNum *= -1;
                rotHackNum = (++rotHackNum) % 4;
            }

        }
        public override void RotLeft(ref GameBoard gb)
        {
            if (checkRotLeft(ref gb))
            {
                Pozice[0, 0] += rotNum * -1;
                Pozice[0, 1] += rotNum * 1;
                Pozice[2, 0] -= rotNum * -1;
                Pozice[2, 1] -= rotNum * 1;
                rotHackNum = (rotHackNum + 3) % 4;
                Pozice[3, 0] -= rotationHack[rotHackNum, 0];
                Pozice[3, 1] -= rotationHack[rotHackNum, 1];
                rotNum *= -1;
            }
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
