using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Vagon
    {
        public int[] coordinates;
        public Vagon next;
        public Vagon(int[] coordinates, Vagon next)
        {
            this.coordinates = coordinates;
            this.next = next;
        }
    }
}
