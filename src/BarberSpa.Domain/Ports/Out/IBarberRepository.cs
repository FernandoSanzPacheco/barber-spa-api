using BarberSpa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Domain.Ports.Out
{
    public interface IBarberRepository : IRepository<Barber>
    {
        Task<IEnumerable<Barber>> GetActiveAsync();
    }
}