﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Ctverec : Shape
    {
        private string nazev = "Ctverec";
        new public char Color = 'R';
        new public int[,] Pozice;
        private int[,] poziceAI;
        public Ctverec()
        {
            Pozice = new int[4, 2] { { 2, 4, }, { 2, 5 }, { 3, 4 }, { 3, 5 } };
        }
        private bool checkDownSide(ref GameBoard gb)
        {
                return (Pozice[3, 0] != 19 && gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == '\0' && gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == '\0');
        }
        private bool checkLeftSide(ref GameBoard gb)
        {
            return (Pozice[0, 1] != 0 && gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == '\0' && gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == '\0');
        }
        private bool checkRightSide(ref GameBoard gb)
        {
            return (Pozice[3, 1] != 9 && gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == '\0' && gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == '\0');
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
        public override void MoveLeft(ref GameBoard gb)
        {
            if (checkLeftSide(ref gb))
            {
                for (int i = 0; i < 4; i++)
                {
                    Pozice[i, 1] -= 1;
                }
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
            }
        }
        public override void RotRight(ref GameBoard gb)
        {
            return;
        }
        public override void RotLeft(ref GameBoard gb)
        {
            return;
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
