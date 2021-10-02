using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public partial class NumberIn
    {
        public int NumberId { get; set; }
        public string NumberIn1 { get; set; }
        public int? PairAts { get; set; }

        public virtual Pair PairAtsNavigation { get; set; }
    }
}
