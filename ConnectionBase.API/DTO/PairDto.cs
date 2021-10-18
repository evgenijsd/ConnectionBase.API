namespace ConnectionBase.API.DTO
{
    public partial class PairDto
    {
        public int PairId { get; set; }
        public int? Cross { get; set; }
        public int PairNum { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
        public bool? PairAb { get; set; }
    }
}
