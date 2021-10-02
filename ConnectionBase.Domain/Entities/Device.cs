using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public partial class Device
    {
        public Device()
        {
            DevicePeople = new HashSet<DevicePerson>();
        }

        public int DeviceId { get; set; }
        public int Model { get; set; }
        public int? Room { get; set; }
        public int? Pair { get; set; }
        public string InvNum { get; set; }

        public virtual DeviceModel ModelNavigation { get; set; }
        public virtual Pair PairNavigation { get; set; }
        public virtual Room RoomNavigation { get; set; }
        public virtual ICollection<DevicePerson> DevicePeople { get; set; }
    }
}
