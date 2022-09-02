using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class VagonPozic
    {
        public int[,] Pozic;
        public VagonPozic next;
        public string navigace;
        public VagonPozic(string navigace, int[,] Pozic, VagonPozic next)
        {
            this.navigace = navigace;
            this.Pozic = Pozic;
            this.next = next;
        }
    }
}
