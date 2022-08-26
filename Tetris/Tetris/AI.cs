using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class AI
    {
        static public int[,,] FindAllHardDrops(ref GameBoard gb, Shape shp)
        {
            int[,,] konec;
            dynamic tvar = shp;
            switch (tvar.Color)
            {
                case 'O':
                    konec = new int[17, 4, 2];
                    break;
                case 'R':
                    konec = new int[9, 4, 2];
                    break;
                case 'D':
                    konec = new int[34, 4, 2];
                    break;
                case 'V':
                    konec = new int[34, 4, 2];
                    break;
                case 'Y':
                    konec = new int[34, 4, 2];
                    break;
                case 'L':
                    konec = new int[17, 4, 2];
                    break;
                case 'G':
                    konec = new int[17, 4, 2];
                    break;
                default:
                    konec = new int[1, 1, 1];
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                tvar.MoveLeft();
            }
            for (int i = 0; i < konec.GetLength(0); i++)
            {

            }
            return konec;
        } 
    }
}
