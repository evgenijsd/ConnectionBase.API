using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.DTO
{
    public class GenerationChains
    {
        public int PairId { get; set; }
        public int? Cross { get; set; }
        public int PairNum { get; set; }
        public int? PairIn { get; set; }
        public int NumChain { get; set; }
        public int PairEnd { get; set; }
        public int? Device { get; set; }
        public int? Building { get; set; }
        public int? Room { get; set; }
    }
}
