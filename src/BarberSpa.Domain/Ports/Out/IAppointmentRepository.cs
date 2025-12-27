using BarberSpa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Domain.Ports.Out
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Appointment>> GetByBarberIdAsync(int barberId);
        // Necesario para validar borrado de Servicios
        Task<IEnumerable<Appointment>> GetByServiceIdAsync(int serviceId);
        Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        // Traer todas las citas CON sus relaciones
        Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    }
}