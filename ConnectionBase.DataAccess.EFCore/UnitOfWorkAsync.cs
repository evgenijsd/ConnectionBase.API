using ConnectionBase.DataAccess.EFCore.Repositories;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System.Threading.Tasks;

namespace ConnectionBase.DataAccess.EFCore
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly ConnectionBaseContext _context;

        public UnitOfWorkAsync(ConnectionBaseContext context)
        {
            _context = context;
            Buildings = new GenericRepositoryAsync<Building>(_context);
            Crosses = new GenericRepositoryAsync<Cross>(_context);
            Departs = new GenericRepositoryAsync<Depart>(_context);
            Devices = new GenericRepositoryAsync<Device>(_context);
            DeviceModels = new GenericRepositoryAsync<DeviceModel>(_context);
            DevicePeople = new GenericRepositoryAsync<DevicePerson>(_context);
            NumberIns = new GenericRepositoryAsync<NumberIn>(_context);
            NumberOuts = new GenericRepositoryAsync<NumberOut>(_context);
            Operators = new GenericRepositoryAsync<Operator>(_context);
            Pairs = new GenericRepositoryAsync<Pair>(_context);
            PairAbs = new GenericRepositoryAsync<PairAb>(_context);
            People = new GenericRepositoryAsync<Person>(_context);
            Rooms = new GenericRepositoryAsync<Room>(_context);
        }

        public IGenericRepositoryAsync<Building> Buildings { get; private set; }
        public IGenericRepositoryAsync<Cross> Crosses { get; private set; }
        public IGenericRepositoryAsync<Depart> Departs { get; private set; }
        public IGenericRepositoryAsync<Device> Devices { get; private set; }
        public IGenericRepositoryAsync<DeviceModel> DeviceModels { get; private set; }
        public IGenericRepositoryAsync<DevicePerson> DevicePeople { get; private set; }
        public IGenericRepositoryAsync<NumberIn> NumberIns { get; private set; }
        public IGenericRepositoryAsync<NumberOut> NumberOuts { get; private set; }
        public IGenericRepositoryAsync<Operator> Operators { get; private set; }
        public IGenericRepositoryAsync<Pair> Pairs { get; private set; }
        public IGenericRepositoryAsync<PairAb> PairAbs { get; private set; }
        public IGenericRepositoryAsync<Person> People { get; private set; }
        public IGenericRepositoryAsync<Room> Rooms { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
