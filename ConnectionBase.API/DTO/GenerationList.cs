namespace ConnectionBase.API.DTO
{
    public class GenerationList
    {
        public int PairBegin { get; set; }
        public int PairEnd { get; set; }
        public int? CrossBegin { get; set; }
        public int? DeviceBegin { get; set; }
        public int PairNumBegin { get; set; }
        public int? BuildingBegin { get; set; }
        public int? RoomBegin { get; set; }
        public int? CrossEnd { get; set; }
        public int? DeviceEnd { get; set; }
        public int PairNumEnd { get; set; }
        public int? BuildingEnd { get; set; }
        public int? RoomEnd { get; set; }
    }
}
