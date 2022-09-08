/*
Tetris
David Kroupa, I. ročník, 31. st. skupina
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
    abstract class Shape
    {
        public int[,] Pozice;//misto, kde se nachazi TetroBlock [4,2]
        public char Color;
        //nasledujici 3 funkce zkontroluji, zda nejsme nahodou na kraji hraci desky at uz vlevo, vpravo ci dole a zkontroluji, jestli
        //muzeme vykonat pohyb danym smerem
        protected bool checkDownSide(ref GameBoard gb)
        {
            return (
                Pozice[0, 0] != 19 && Pozice[1, 0] != 19 &&
                Pozice[2, 0] != 19 && Pozice[3, 0] != 19 &&
                gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] == '\0' &&
                gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] == '\0' &&
                gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == '\0' &&
                gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == '\0');

        }
        protected bool checkLeftSide(ref GameBoard gb)
        {
            return (
                Pozice[0, 1] != 0 && Pozice[1, 1] != 0 &&
                Pozice[2, 1] != 0 && Pozice[3, 1] != 0 &&
                gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == '\0' &&
                gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == '\0' &&
                gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == '\0' &&
                gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == '\0');


        }
        protected bool checkRightSide(ref GameBoard gb)
        {
            return (
                Pozice[0, 1] != 9 && Pozice[1, 1] != 9 &&
                Pozice[2, 1] != 9 && Pozice[3, 1] != 9 &&
                gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == '\0' &&
                gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == '\0' &&
                gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == '\0' &&
                gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == '\0');


        }
        //dalsi 3 funkce jsou totozne, jako predchozi 3 s rozdilem, ze int[,] Pozice je dana jako parametr a ne jako atribut Shape tridy
        //take to povoluje,aby v hracim poli bylo policko obsazeno charem 'F', coz symbolizue dFs prohledavani, avsak nesmi to byt obsazene 'F' cele
        static public bool checkDownSide(ref GameBoard gb, int[,] Pozice)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetrisDFS nebo tetrisBFS
            return (
                Pozice[0, 0] != 19 && Pozice[1, 0] != 19 &&
                Pozice[2, 0] != 19 && Pozice[3, 0] != 19 &&
                (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != 'F' || gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != 'F' ||
                gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != 'F' || gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != 'F') &&
                (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] == '\0' || gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] == 'F') &&
                (gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] == '\0' || gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] == 'F') &&
                (gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == '\0' || gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] == 'F') &&
                (gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == '\0' || gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] == 'F'));
        }
        static public bool checkLeftSide(ref GameBoard gb, int[,] Pozice)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetrisDFS nebo tetrisBFS
            return (
                Pozice[0, 1] != 0 && Pozice[1, 1] != 0 &&
                Pozice[2, 1] != 0 && Pozice[3, 1] != 0 &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] != 'F' || gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] != 'F' ||
                gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] != 'F' || gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] != 'F') &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == '\0' || gb.Board[Pozice[0, 0], Pozice[0, 1] - 1] == 'F') &&
                (gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == '\0' || gb.Board[Pozice[1, 0], Pozice[1, 1] - 1] == 'F') &&
                (gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == '\0' || gb.Board[Pozice[2, 0], Pozice[2, 1] - 1] == 'F') &&
                (gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == '\0' || gb.Board[Pozice[3, 0], Pozice[3, 1] - 1] == 'F'));

        }
        static public bool checkRightSide(ref GameBoard gb, int[,] Pozice)
        {
            //'\0' je prazdne misto, 'F' je char oznacujici jiz projdenou pozici pri tetrisDFS nebo tetrisBFS
            return (
                Pozice[0, 1] != 9 && Pozice[1, 1] != 9 &&
                Pozice[2, 1] != 9 && Pozice[3, 1] != 9 &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] != 'F' || gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] != 'F' ||
                gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] != 'F' || gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] != 'F') &&
                (gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == '\0' || gb.Board[Pozice[0, 0], Pozice[0, 1] + 1] == 'F') &&
                (gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == '\0' || gb.Board[Pozice[1, 0], Pozice[1, 1] + 1] == 'F') &&
                (gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == '\0' || gb.Board[Pozice[2, 0], Pozice[2, 1] + 1] == 'F') &&
                (gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == '\0' || gb.Board[Pozice[3, 0], Pozice[3, 1] + 1] == 'F'));

        }
        //standardni funkce jako pohyb doleva, doprava, dolu, rotace nebo zhozeni az na sami spodek hraci desky
        public abstract void MoveUp();
        public abstract bool MoveDown(ref GameBoard gb);
        public abstract bool MoveLeft(ref GameBoard gb);
        public abstract bool MoveRight(ref GameBoard gb);
        public abstract void RotLeft(ref GameBoard gb);
        public abstract bool RotRight(ref GameBoard gb);
        //hodi TetroBlock co nejvic dolu, jak to bude mozne a vrati pocet posunu dolu (dulezite info pro score hry)
        public  int HardDrop(ref GameBoard gb)
        {
            int pocet = 0;
            while (MoveDown(ref gb))
            {
                ++pocet;
            }
            return pocet;
        }
        //funkce pro nalezeni souradnic pri HardDropu bez nutnosti pohybovat s TetroBlockem
        public int[,] FakeHardDrop(ref GameBoard gb)
        {

            int[,] poziceAI = (int[,])Pozice.Clone();
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
        //oznaceni jiz navstiveneho mista v desce
        public void markVisited(ref GameBoard gb)
        {
            for (int i = 0; i < 4; i++)
            {
                gb.Board[Pozice[i, 0], Pozice[i, 1]] = 'F';//tetris dFs, bFs
            }
        }
        //2 stejne funkce, ktere vrati true v pripade ze muzeme pohnout se souradnicemi dolu a nove souradnice nejsou cele pokryte 'F' chary
        public bool MoveDownNotPossible(ref GameBoard gb)
        {
            return (
               Pozice[0, 0] == 19 || Pozice[1, 0] == 19 ||
               Pozice[2, 0] == 19 || Pozice[3, 0] == 19 ||
               (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != '\0' && gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != 'F') ||
               (gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != '\0' && gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != 'F') ||
               (gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != '\0' && gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != 'F') ||
               (gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != '\0' && gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != 'F'));

        }
        public static bool MoveDownNotPossible(ref GameBoard gb, int[,] Pozice)
        {
            return (
               Pozice[0, 0] == 19 || Pozice[1, 0] == 19 ||
               Pozice[2, 0] == 19 || Pozice[3, 0] == 19 ||
               (gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != '\0' && gb.Board[Pozice[0, 0] + 1, Pozice[0, 1]] != 'F') ||
               (gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != '\0' && gb.Board[Pozice[1, 0] + 1, Pozice[1, 1]] != 'F') ||
               (gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != '\0' && gb.Board[Pozice[2, 0] + 1, Pozice[2, 1]] != 'F') ||
               (gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != '\0' && gb.Board[Pozice[3, 0] + 1, Pozice[3, 1]] != 'F'));

        }
        //podle barvy poznam pocet nutnych a moznych ruznych rotaci
        public int NumOfRots()
        {
            switch (Color)
            {
                case 'O':
                    return 1;
                case 'R':
                    return 0;
                case 'D':
                    return 3;
                case 'V':
                    return 3;
                case 'Y':
                    return 3;
                case 'L':
                    return 1;
                case 'G':
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
