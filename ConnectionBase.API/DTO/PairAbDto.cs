namespace ConnectionBase.API.DTO
{
    public partial class PairAbDto
    {
        public int AbId { get; set; }
        public int Pair { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
    }
}
