/*
Tetris
David Kroupa, I. ročník, 31 st. skupina
letní semestr 2021/22
Programování 2 NPRG031
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tecko : Shape
    {
        //dana figurka je reprezentovana jako symbol plus '+' bez jednoho hrbolu, ktery se pohybuje pomoci poziceDiry pole a rotNum hodnoty
        private int[] stred;
        private int[,] poziceDiry;
        int rotNum;
        public Tecko()
        {
            Pozice = new int[4, 2] { { 2, 4 }, { 2, 3, }, { 2, 5 }, { 3, 4 } };
            stred = new int[2] { 2, 4 };
            poziceDiry = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
            rotNum = 0;
            Color = 'Y';
        }
        private bool checkRot(ref GameBoard gb)
        {
            //pri rotaci vzdy jen 'odstranime jeden roh a pridame ho na jinou pozici
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
            if (checkRot(ref gb))
            {
                rotNum = (++rotNum) % 4;
                for (int i = 1; i < 4; i++)
                {
                    Pozice[i, 0] = stred[0] + poziceDiry[(rotNum +i ) % 4, 0];
                    Pozice[i, 1] = stred[1] + poziceDiry[(rotNum + i) % 4, 1];
                }
                return true;
            }
            else
            {
                return false;
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
