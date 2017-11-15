using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class ReorderBuffer
    {
        public bool Done { get; set; }
        public bool Exception { get; set; }
        public int RegisterFile { get; set; }
        public int Value { get; set; }
        public int Index { get; set; }

        public ReorderBuffer()
        {
            this.Done = false;
            this.Exception = false;
        }
    }
}
