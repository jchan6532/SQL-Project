using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationSimulator
{
    internal class OrderSession
    {
        public int WorkstationId { get; }
        public int OrderId { get; }
        public int LampsBuilt { get; }
        public int DefectsBuilt { get; }

        public OrderSession(int workstationId, int orderId, int lampsBuilt, int defectsBuilt)
        {
            WorkstationId = workstationId;
            OrderId = orderId;
            LampsBuilt = lampsBuilt;
            DefectsBuilt = defectsBuilt;
        }
    }
}
