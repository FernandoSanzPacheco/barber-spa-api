using BarberSpa.Application.DTOs.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAsync(int userId, CreateAppointmentDto dto);
        Task<IEnumerable<AppointmentDto>> GetMyAppointmentsAsync(int userId);
        Task<IEnumerable<AppointmentDto>> GetAllAsync(); // Para Admin/Receptionist
        Task<AppointmentDto> UpdateStatusAsync(int id, string status); // Para Admin/Receptionist cambiar estado
        Task<bool> CancelAsync(int id, int userId, string userRole); // Cancelación con validación de roles
        Task<bool> DeleteAsync(int id); // Eliminación física (Admin)
    }
}