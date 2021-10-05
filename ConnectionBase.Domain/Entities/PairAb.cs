using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class PairAb
    {
        public int AbId { get; set; }
        public int Pair { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }

        public virtual Pair PairNavigation { get; set; }
    }
}
