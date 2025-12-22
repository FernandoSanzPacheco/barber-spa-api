using BarberSpa.Application.DTOs.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAsync(int userId, CreateAppointmentDto dto); // UserId viene del Token
        Task<IEnumerable<AppointmentDto>> GetMyAppointmentsAsync(int userId);
        Task<bool> CancelAsync(int id);
    }
}