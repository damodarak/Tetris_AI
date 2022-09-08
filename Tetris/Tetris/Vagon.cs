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
    class Vagon
    {
        //Vagon pro DS Queue
        public int[] coordinates;
        public Vagon next;
        public Vagon(int[] coordinates, Vagon next)
        {
            this.coordinates = coordinates;
            this.next = next;
        }
    }
}
