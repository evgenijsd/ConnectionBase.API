using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service.Interface
{
    public interface IChainServiceAsync<Tlist, Tchain>
    {
        Task<List<Tchain>> GetAllChainAsync();
        Task<List<Tchain>> GetChainAsync(int pairEndId);
        Task<List<Tlist>> GetListChainsAsync();
    }
}
