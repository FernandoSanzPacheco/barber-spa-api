using BarberSpa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Domain.Ports.Out
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetActiveAsync();
    }
}