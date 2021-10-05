namespace ConnectionBase.Domain.Entities
{
    public partial class PairDto
    {
        public int ParaId { get; set; }
        public int? Cross { get; set; }
        public int PairNum { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
        public bool? ConnectionAb { get; set; }
    }
}
