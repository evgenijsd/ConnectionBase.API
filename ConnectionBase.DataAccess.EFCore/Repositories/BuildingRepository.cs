using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.DataAccess.EFCore.Repositories
{
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(ConnectionBaseContext context) : base(context)
        {
        }
    }
}
