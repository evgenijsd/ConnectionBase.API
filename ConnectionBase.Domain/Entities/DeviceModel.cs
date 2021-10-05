using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class DeviceModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
