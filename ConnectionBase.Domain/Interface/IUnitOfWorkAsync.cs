using ConnectionBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Interface
{
    public interface IUnitOfWorkAsync : IAsyncDisposable
    {
        IGenericRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        /* IGenericRepositoryAsync<Building> Buildings { get; }
         IGenericRepositoryAsync<Cross> Crosses { get; }
         IGenericRepositoryAsync<Depart> Departs { get; }
         IGenericRepositoryAsync<Device> Devices { get; }
         IGenericRepositoryAsync<DeviceModel> DeviceModels { get; }
         IGenericRepositoryAsync<DevicePerson> DevicePeople { get; }
         IGenericRepositoryAsync<NumberIn> NumberIns { get; }
         IGenericRepositoryAsync<NumberOut> NumberOuts { get; }
         IGenericRepositoryAsync<Operator> Operators { get; }
         IGenericRepositoryAsync<Pair> Pairs { get; }
         IGenericRepositoryAsync<PairAb> PairAbs { get; }
         IGenericRepositoryAsync<Person> People { get; }
         IGenericRepositoryAsync<Room> Rooms { get; }*/

        Task<int> CompleteAsync();
    }
}
