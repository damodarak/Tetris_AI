using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Jecko : Shape
    {
        //dana figurka je reprezentovana svym stredem a 'hackem', ktery se pohybuje podle hodnoty rotNum
        private int[,] rotationHack;
        private int[] stred;
        private int rotNum;
        private int rotHackNum;
        public Jecko()
        {
            Pozice = new int[4, 2] { { 2, 3 }, { 2, 4 }, { 2, 5 }, { 3, 5 } };//rovna primka a na konci hacek
            rotationHack = new int[4, 2] { { 0, -2 }, { -2, 0 }, { 0, 2 }, { 2, 0 } };//pohyb hracku pri rotaci
            stred = new int[2] { 2, 4 };
            rotNum = 1;
            rotHackNum = 0;
            Color = 'V';
        }
        private bool checkRotRight(ref GameBoard gb)
        {
            return (stred[0] != 19 && stred[1] != 0 && stred[1] != 9 &&
                gb.Board[Pozice[0, 0] + (rotNum * -1), Pozice[0, 1] + (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[2, 0] - (rotNum * -1), Pozice[2, 1] - (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[3,0] + rotationHack[rotHackNum,0], Pozice[3,1] + rotationHack[rotHackNum,1]] == '\0');
        }
        private bool checkRotLeft(ref GameBoard gb)
        {
            return (stred[0] != 19 && stred[1] != 0 && stred[1] != 9 &&
                gb.Board[Pozice[0, 0] + (rotNum * -1), Pozice[0, 1] + (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[2, 0] - (rotNum * -1), Pozice[2, 1] - (rotNum * 1)] == '\0' &&
                gb.Board[Pozice[3, 0] - rotationHack[(rotHackNum + 3 ) % 4, 0], Pozice[3, 1] - rotationHack[(rotHackNum + 3 ) % 4, 1]] == '\0');
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
            if (checkDownSide(ref gb))
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
        public override bool MoveLeft(ref GameBoard gb)
        {
            if (checkLeftSide(ref gb))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] -= 1;
                }
                stred[1] -= 1;
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
                stred[1] += 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool RotRight(ref GameBoard gb)
        {
            if (checkRotRight(ref gb))
            {
                //pokud to bude mozne, tak primku otocime o 90° a hacek posuneme podle posunu a hodnoty rotHackNum
                Pozice[0, 0] += rotNum * -1;
                Pozice[0, 1] += rotNum * 1;
                Pozice[2, 0] -= rotNum * -1;
                Pozice[2, 1] -= rotNum * 1;
                Pozice[3, 0] += rotationHack[rotHackNum, 0];
                Pozice[3, 1] += rotationHack[rotHackNum, 1];
                rotNum *= -1;
                rotHackNum = (++rotHackNum) % 4;
                return true;
            }
            return false;
        }
        public override void RotLeft(ref GameBoard gb)
        {
            //stejna logika jako v predchozi funkci
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
    }
}
