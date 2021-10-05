using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class Operator
    {
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }

        public virtual ICollection<NumberOut> NumberOuts { get; set; }
    }
}
