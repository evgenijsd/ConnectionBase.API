using ConnectionBase.DataAccess.EFCore.Repositories;
using ConnectionBase.Domain.Interface;

namespace ConnectionBase.DataAccess.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConnectionBaseContext _context;

        public UnitOfWork(ConnectionBaseContext context)
        {
            _context = context;
            Buildings = new BuildingRepository(_context);
            Crosses = new CrossRepository(_context);
            Departs = new DepartRepository(_context);
            Devices = new DeviceRepository(_context);
            DeviceModels = new DeviceModelRepository(_context);
            DevicePeople = new DevicePersonRepository(_context);
            NumberIns = new NumberInRepository(_context);
            NumberOuts = new NumberOutRepository(_context);
            Operators = new OperatorRepository(_context);
            Pairs = new PairRepository(_context);
            PairAbs = new PairAbRepository(_context);
            People = new PersonRepository(_context);
            Rooms = new RoomRepository(_context);
        }

        public IBuildingRepository Buildings { get; private set; }
        public ICrossRepository Crosses { get; private set; }
        public IDepartRepository Departs { get; private set; }
        public IDeviceRepository Devices { get; private set; }
        public IDeviceModelRepository DeviceModels { get; private set; }
        public IDevicePersonRepository DevicePeople { get; private set; }
        public INumberInRepository NumberIns { get; private set; }
        public INumberOutRepository NumberOuts { get; private set; }
        public IOperatorRepository Operators { get; private set; }
        public IPairRepository Pairs { get; private set; }
        public IPairAbRepository PairAbs { get; private set; }
        public IPersonRepository People { get; private set; }
        public IRoomRepository Rooms { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
