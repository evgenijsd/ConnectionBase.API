using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class DevicePersonRepository : GenericRepository<DevicePerson>, IDevicePersonRepository
    {
        public DevicePersonRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
