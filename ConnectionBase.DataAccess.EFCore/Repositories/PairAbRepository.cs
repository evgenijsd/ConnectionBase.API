using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class PairAbRepository : GenericRepository<PairAb>, IPairAbRepository
    {
        public PairAbRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
