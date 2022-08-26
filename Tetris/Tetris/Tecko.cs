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
        private int[,] poziceAI;
        public Tecko()
        {
            Pozice = new int[4, 2] { { 2, 4 }, { 2, 3, }, { 2, 5 }, { 3, 4 } };
            stred = new int[2] { 2, 4 };
            poziceDiry = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
            rotNum = 0;
        }
        private bool checkRot(ref GameBoard gb)
        {
            return (stred[1] != 0 && stred[1] != 9 && stred[0] != 19 &&
                gb.Board[stred[0] + poziceDiry[rotNum, 0], stred[1] + poziceDiry[rotNum, 1]] == '\0');
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
        public override int HardDrop(ref GameBoard gb)
        {
            int pocet = 0;
            while (MoveDown(ref gb))
            {
                ++pocet;
            }
            return pocet;
        }
        public override int[,] FakeHardDrop(ref GameBoard gb)
        {

            poziceAI = (int[,])Pozice.Clone();
            while (poziceAI[0, 0] != 19 && poziceAI[1, 0] != 19 &&
                poziceAI[2, 0] != 19 && poziceAI[3, 0] != 19 &&
                gb.Board[poziceAI[0, 0] + 1, poziceAI[0, 1]] == '\0' &&
                gb.Board[poziceAI[1, 0] + 1, poziceAI[1, 1]] == '\0' &&
                gb.Board[poziceAI[2, 0] + 1, poziceAI[2, 1]] == '\0' &&
                gb.Board[poziceAI[3, 0] + 1, poziceAI[3, 1]] == '\0')
            {
                for (int i = 0; i < 4; i++)
                {
                    poziceAI[i, 0] += 1;
                }
            }
            return poziceAI;
        }
    }
}
