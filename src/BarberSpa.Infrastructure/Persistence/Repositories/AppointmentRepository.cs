using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Ports.Out;
using BarberSpa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberSpa.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(a => a.UserId == userId)
                .Include(a => a.Barber)
                .Include(a => a.Service)
                .Include(a => a.User)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByBarberIdAsync(int barberId)
        {
            return await _dbSet
                .Where(a => a.BarberId == barberId)
                .Include(a => a.User)
                .Include(a => a.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByServiceIdAsync(int serviceId)
        {
            return await _dbSet
                .Where(a => a.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .Include(a => a.User)
                .Include(a => a.Barber)
                .Include(a => a.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(a => a.User)    // JOIN con Users (Cliente)
                .Include(a => a.Barber)  // JOIN con Barbers
                .Include(a => a.Service) // JOIN con Services
                .OrderByDescending(a => a.AppointmentDate) // Ordenar por fecha
                .ToListAsync();
        }
    }
}