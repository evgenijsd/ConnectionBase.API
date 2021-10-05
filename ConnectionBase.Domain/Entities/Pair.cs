using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class Pair
    {
        public int ParaId { get; set; }
        public int? Cross { get; set; }
        public int PairNum { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
        public bool? ConnectionAb { get; set; }

        public virtual Cross CrossNavigation { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<NumberIn> NumberIns { get; set; }
        public virtual ICollection<NumberOut> NumberOuts { get; set; }
        public virtual ICollection<PairAb> PairAbs { get; set; }
    }
}
