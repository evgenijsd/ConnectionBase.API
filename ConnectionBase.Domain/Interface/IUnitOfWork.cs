using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBuildingRepository Buildings { get; }
        ICrossRepository Crosses { get; }
        IDepartRepository Departs { get; }
        IDeviceRepository Devices { get; }
        IDeviceModelRepository DeviceModels { get; }
        IDevicePersonRepository DevicePeople { get; }
        INumberInRepository NumberIns { get; }
        INumberOutRepository NumberOuts { get; }
        IOperatorRepository Operators { get; }
        IPairRepository Pairs { get; }
        IPairAbRepository PairAbs { get; }
        IPersonRepository People { get; }
        IRoomRepository Rooms { get; }

        int Complete();
    }
}
