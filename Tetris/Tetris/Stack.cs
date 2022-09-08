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
    class Stack
    {
        //puvodne pro DFS prohledavani pri hledani vsech tahu, ale nakonec nepouzita trida
        VagonPozic head;
        int count;
        public Stack()
        {
            this.head = null;
            count = 0;
        }
        public int Count()
        {
            return this.count;
        }
        public void Insert(int[,] val, string navigace)
        {
            if (this.head == null)
            {
                this.head = new VagonPozic(navigace, val, null);
            }
            else
            {
                VagonPozic pom = new VagonPozic(navigace, val, head);
                this.head = pom;
            }
            ++this.count;
        }
        //pretizeni funkce Insert pro moznost vlozit pomoci objektu InfoBlock
        public void Insert(InfoBlock ib)
        {
            if (this.head == null)
            {
                this.head = new VagonPozic(ib.StringValue, ib.ArrayValue, null);
            }
            else
            {
                VagonPozic pom = new VagonPozic(ib.StringValue, ib.ArrayValue, head);
                this.head = pom;
            }
            ++this.count;
        }
        public InfoBlock Pop()
        {
            int[,] pozice = this.head.Pozic;
            string nav = this.head.navigace;
            if (this.count == 1)
            {
                this.head = null;
            }
            else
            {
                this.head = this.head.next;
            }
            --this.count;
            return new InfoBlock(nav, pozice);
        }
    }
}
