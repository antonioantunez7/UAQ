using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecUAQ.Services
{
    public interface InterfaceEventosDataStore<T>
    {
        Task<IEnumerable<T>> getEventos(bool forceRefresh = false);
    }
}
