using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class DevicePerson
    {
        public int DevicePersonId { get; set; }
        public int Device { get; set; }
        public int? Person { get; set; }

        public virtual Device DeviceNavigation { get; set; }
        public virtual Person PersonNavigation { get; set; }
    }
}
