namespace ConnectionBase.API.DTO
{
    public partial class DeviceDto
    {
        public int DeviceId { get; set; }
        public int Model { get; set; }
        public int? Room { get; set; }
        public int? Pair { get; set; }
        public string InvNum { get; set; }
    }
}
