using System;
using System.Collections.Generic;

#nullable disable

namespace ConnectionBase.Domain.Entities
{
    public class Building
    {
        public Building()
        {
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
