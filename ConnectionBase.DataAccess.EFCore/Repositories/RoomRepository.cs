using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
