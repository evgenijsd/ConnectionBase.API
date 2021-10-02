using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class OperatorRepository : GenericRepository<Operator>, IOperatorRepository
    {
        public OperatorRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
