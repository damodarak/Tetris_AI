using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tecko : Shape
    {
        private string nazev = "Tecko";
        new public char Color = 'Y';
        new public int[,] Pozice;
        private int[] stred;
        private int[,] poziceDiry;
        int rotNum;
        public Tecko()
        {
            Pozice = new int[4, 2] { { 2, 4 }, { 2, 3, }, { 2, 5 }, { 3, 4 } };
            stred = new int[2] { 2, 4 };
            poziceDiry = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
            rotNum = 0;
        }
        private bool checkDownSide(ref GameBoard gb)
        {
            return (
                Pozice[0,0] != 19 && Pozice[1,0] != 19 &&
                Pozice[2,0] != 19 && Pozice[3,0] != 19 &&
                gb.Board[Pozice[0,0] + 1, Pozice[0,1]] == '\0' &&
                gb.Board[Pozice[1,0] + 1, Pozice[1,1]] == '\0' &&
                gb.Board[Pozice[2,0] + 1, Pozice[2,1]] == '\0' &&
                gb.Board[Pozice[3,0] + 1, Pozice[3,1]] == '\0');
        }
        private bool checkLeftSide(ref GameBoard gb)
        {
            return (
                Pozice[0, 1] != 0 && Pozice[1, 1] != 0 &&
                Pozice[2, 1] != 0 && Pozice[3, 1] != 0 &&
                gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == '\0' &&
                gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == '\0' &&
                gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == '\0' &&
                gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == '\0');
        }
        private bool checkRightSide(ref GameBoard gb)
        {
            return (
                Pozice[0, 1] != 9 && Pozice[1, 1] != 9 &&
                Pozice[2, 1] != 9 && Pozice[3, 1] != 9 &&
                gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == '\0' &&
                gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == '\0' &&
                gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == '\0' &&
                gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == '\0');
        }
        private bool checkRot(ref GameBoard gb)
        {
            return (stred[1] != 0 && stred[1] != 9 && stred[0] != 19 &&
                gb.Board[stred[0] + poziceDiry[rotNum, 0], stred[1] + poziceDiry[rotNum, 1]] == '\0');
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
        public override void MoveLeft(ref GameBoard gb)
        {
            if (checkLeftSide(ref gb))
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
            if (checkRightSide(ref gb))
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
            if (checkRot(ref gb))
            {
                rotNum = (++rotNum) % 4;
                for (int i = 1; i < 4; i++)
                {
                    Pozice[i, 0] = stred[0] + poziceDiry[(rotNum +i ) % 4, 0];
                    Pozice[i, 1] = stred[1] + poziceDiry[(rotNum + i) % 4, 1];
                }
            }

        }
        public override void RotLeft(ref GameBoard gb)
        {
            if (checkRot(ref gb))
            {
                rotNum = (rotNum + 3) % 4;
                for (int i = 1; i < 4; i++)
                {
                    Pozice[i, 0] = stred[0] + poziceDiry[(rotNum + i) % 4, 0];
                    Pozice[i, 1] = stred[1] + poziceDiry[(rotNum + i) % 4, 1];
                }
            }
        }
    }
}
