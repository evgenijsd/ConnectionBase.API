using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class Cross
    {
        public int CrossId { get; set; }
        public string CrossName { get; set; }
        public int BeginNum { get; set; }
        public int NumberPair { get; set; }
        public bool? Ats { get; set; }
        public int? Room { get; set; }

        public virtual Room RoomNavigation { get; set; }
        public virtual ICollection<Pair> Pairs { get; set; }

        public static implicit operator int(Cross v)
        {
            throw new NotImplementedException();
        }
    }
}
