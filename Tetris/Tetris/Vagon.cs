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
