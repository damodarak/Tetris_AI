using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class AI
    {
        static public void tetrisDFS(ref GameBoard gb, Shape shp, Stack st, string navigace)
        {
            if (!shp.MoveDown(ref gb))
            {
                st.Insert(shp.Pozice, navigace);
                return;
            }
            else
            {
                shp.MoveUp();

                if (shp.MoveLeft(ref gb))
                {
                    shp.markVisited(ref gb);
                    navigace += 'L';
                    tetrisDFS(ref gb, shp, st, navigace);
                    shp.MoveRight(ref gb);
                }
                if (shp.MoveRight(ref gb))
                {
                    shp.markVisited(ref gb);
                    navigace += 'R';
                    tetrisDFS(ref gb, shp, st, navigace);
                    shp.MoveLeft(ref gb);
                }
                if (shp.MoveDown(ref gb))
                {
                    shp.markVisited(ref gb);
                    navigace += 'D';
                    tetrisDFS(ref gb, shp, st, navigace);
                    shp.MoveUp();
                }
            }
        }
    }
}
