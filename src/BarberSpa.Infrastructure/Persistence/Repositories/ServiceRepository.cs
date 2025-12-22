using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Ports.Out;
using BarberSpa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberSpa.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Service>> GetActiveAsync()
        {
            return await _dbSet.Where(s => s.IsActive).ToListAsync();
        }
    }
}