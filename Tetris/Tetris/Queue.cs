using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Queue
    {
        //standardni trida, ktera je pouzita pri pocitani zablokovanych mist v hraci desce
        Vagon head;
        Vagon tail;
        int count;
        public Queue()
        {
            this.head = null;
            this.tail = null;
            count = 0;
        }
        public bool Count()
        {
            if (count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Insert(int[] val)
        {
            if (this.head == null)
            {
                this.head = new Vagon(val, null);
                this.tail = this.head;
            }
            else
            {
                Vagon pom = new Vagon(val, null);
                this.tail.next = pom;
                this.tail = pom;
            }
            ++this.count;
        }
        public int[] Pop()
        {
            int[] konec = this.head.coordinates;
            if (this.tail == this.head)
            {
                this.tail = this.head = null;
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
