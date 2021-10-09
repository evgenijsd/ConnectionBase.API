namespace ConnectionBase.API.DTO
{
    public partial class CrossDto
    {
        public int CrossId { get; set; }
        public string CrossName { get; set; }
        public int BeginNum { get; set; }
        public int NumberPair { get; set; }
        public bool? Ats { get; set; }
        public int? Room { get; set; }
    }
}
