using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class DeviceModelRepository : GenericRepository<DeviceModel>, IDeviceModelRepository
    {
        public DeviceModelRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
