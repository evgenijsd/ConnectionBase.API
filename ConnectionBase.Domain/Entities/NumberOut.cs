using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class NumberOut
    {
        public int NumberId { get; set; }
        public string Number_Out { get; set; }
        public int? PairAts { get; set; }
        public int? Operator { get; set; }

        public virtual Operator OperatorNavigation { get; set; }
        public virtual Pair PairAtsNavigation { get; set; }
    }
}
