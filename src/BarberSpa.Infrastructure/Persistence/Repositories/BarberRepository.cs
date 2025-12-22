using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Ports.Out;
using BarberSpa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberSpa.Infrastructure.Persistence.Repositories
{
    public class BarberRepository : Repository<Barber>, IBarberRepository
    {
        public BarberRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Barber>> GetActiveAsync()
        {
            return await _dbSet.Where(b => b.IsActive).ToListAsync();
        }
    }
}