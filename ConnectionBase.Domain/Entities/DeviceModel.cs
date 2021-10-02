using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public partial class DeviceModel
    {
        public DeviceModel()
        {
            Devices = new HashSet<Device>();
        }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
