using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service.Interface
{
    public interface ICrossServiceAsync<T, Tdto> : IGenericServiceAsync<T, Tdto>
    {
    }
}
