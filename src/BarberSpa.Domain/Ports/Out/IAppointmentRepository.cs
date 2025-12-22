using BarberSpa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BarberSpa.Domain.Ports.Out
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Appointment>> GetByBarberIdAsync(int barberId);
        Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}