using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string Position { get; set; }
        public int? Depart { get; set; }

        public virtual Depart DepartNavigation { get; set; }
        public virtual ICollection<DevicePerson> DevicePeople { get; set; }
    }
}
