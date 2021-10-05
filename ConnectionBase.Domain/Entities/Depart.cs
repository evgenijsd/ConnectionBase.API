using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class Depart
    {
        public int DepartId { get; set; }
        public string DepartName { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
