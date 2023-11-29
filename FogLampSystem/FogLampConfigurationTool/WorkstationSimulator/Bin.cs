using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class Bin
    {
        public int BinId { get; set; }
        public int PartId { get; set; }
        public int Count { get; set; }
        public int RefillAmount { get; set; }
        public string PartName { get; set; }
    }
}
