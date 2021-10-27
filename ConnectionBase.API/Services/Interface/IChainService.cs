using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.API.Services.Interface
{
    public interface IChainService
    {
        Task<List<GenerationChains>> GetAllChainAsync();
        Task<List<GenerationChains>> GetChainAsync(int pairEndId);
        Task<List<GenerationList>> GetListChainsAsync();
    }
}
