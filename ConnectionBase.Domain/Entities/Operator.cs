using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public partial class Operator
    {
        public Operator()
        {
            NumberOuts = new HashSet<NumberOut>();
        }

        public int OperatorId { get; set; }
        public string OperatorName { get; set; }

        public virtual ICollection<NumberOut> NumberOuts { get; set; }
    }
}
