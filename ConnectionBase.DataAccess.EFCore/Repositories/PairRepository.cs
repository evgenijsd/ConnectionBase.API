using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class PairRepository : GenericRepository<Pair>, IPairRepository
    {
        public PairRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
