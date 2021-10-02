using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class DepartRepository : GenericRepository<Depart>, IDepartRepository
    {
        public DepartRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
