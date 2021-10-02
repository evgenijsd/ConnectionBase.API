using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public partial class Room
    {
        public Room()
        {
            Crosses = new HashSet<Cross>();
            Devices = new HashSet<Device>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Building { get; set; }

        public virtual Building BuildingNavigation { get; set; }
        public virtual ICollection<Cross> Crosses { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
