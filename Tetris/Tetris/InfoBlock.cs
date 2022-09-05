using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class InfoBlock
    {
        //objekt, ktery uchovava cestu k pozici od startu a pozici samotnou v poli ArrayValue
        public string StringValue;
        public int[,] ArrayValue;
        public InfoBlock(string val1, int[,] val2)
        {
            this.StringValue = val1;
            this.ArrayValue = val2;
        }
    }
}
