using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Stack
    {
        VagonPozic head;
        int count;
        public Stack()
        {
            this.head = null;
            count = 0;
        }
        public bool Count()
        {
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public int[,] Pop()
        {
            int[,] konec = this.head.Pozic;
            if (this.count == 1)
            {
                this.head = null;
            }
            else
            {
                this.head = this.head.next;
            }
            --this.count;
            return konec;
        }
    }
}
