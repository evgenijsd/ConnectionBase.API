using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class NumberInRepository : GenericRepository<NumberIn>, INumberInRepository
    {
        public NumberInRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
