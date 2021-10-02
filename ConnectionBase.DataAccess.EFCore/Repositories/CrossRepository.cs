using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class CrossRepository : GenericRepository<Cross>, ICrossRepository
    {
        public CrossRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
