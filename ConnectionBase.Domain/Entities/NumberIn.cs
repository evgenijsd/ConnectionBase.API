using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class NumberIn
    {
        public int NumberId { get; set; }
        public string Number_In { get; set; }
        public int? PairAts { get; set; }

        public virtual Pair PairAtsNavigation { get; set; }
    }
}
