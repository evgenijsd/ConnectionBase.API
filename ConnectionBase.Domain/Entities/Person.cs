using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Entities
{
    public partial class Person
    {
        public Person()
        {
            DevicePeople = new HashSet<DevicePerson>();
        }

        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string Position { get; set; }
        public int? Depart { get; set; }

        public virtual Depart DepartNavigation { get; set; }
        public virtual ICollection<DevicePerson> DevicePeople { get; set; }
    }
}
