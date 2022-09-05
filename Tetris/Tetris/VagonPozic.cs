using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class VagonPozic
    {
        //Vagon pro DS Stack a QueuePozic
        public int[,] Pozic;
        public VagonPozic next;
        public string navigace;
        public VagonPozic(string navigace, int[,] Pozic, VagonPozic next)
        {
            this.navigace = navigace;
            this.Pozic = (int[,])Pozic.Clone();//je pouzita funkce Clone(), abychom zabranili prepisovani pomoci reference
            this.next = next;
        }
    }
}
