using AutoMapper;
using BarberSpa.Application.DTOs.Appointment;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Exceptions;
using BarberSpa.Domain.Ports.Out;
using BarberSpa.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Crear Cita (Cliente o Admin para si mismo)
        public async Task<AppointmentDto> CreateAsync(int userId, CreateAppointmentDto dto)
        {
            if (dto.AppointmentDate <= DateTime.Now)
                throw new BusinessRuleException("InvalidDate", "La fecha debe ser futura.");

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.UserId = userId;
            appointment.Status = "Scheduled";

            await _unitOfWork.Appointments.CreateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            // Retornamos la entidad mapeada (para tener nombres de relaciones si el mapper lo permite)
            return _mapper.Map<AppointmentDto>(appointment);
        }

        // Cliente: Ver solo sus citas
        public async Task<IEnumerable<AppointmentDto>> GetMyAppointmentsAsync(int userId)
        {
            var appointments = await _unitOfWork.Appointments.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        // Obtener todas las citas (Admin / Receptionist)
        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        // Cancelar Cita (Logica condicional por Rol)
        public async Task<bool> CancelAsync(int id, int userId, string userRole)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) throw new NotFoundException("Cita", id);

            // Reglas para CLIENTES
            if (userRole == Roles.Client)
            {
                // Solo puede cancelar sus propias citas
                if (appointment.UserId != userId)
                    throw new BusinessRuleException("Forbidden", "No tienes permiso para gestionar esta cita.");

                // Solo puede cancelar si es futura y está agendada
                if (!appointment.CanBeCancelled())
                    throw new BusinessRuleException("LateCancellation", "No se puede cancelar citas pasadas o ya completadas.");
            }

            // Admin y Receptionist pueden cancelar cualquier cita en cualquier estado

            appointment.Status = "Cancelled";
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // Admin/Receptionist: Cambiar estado (Completada, No Asistio)
        public async Task<AppointmentDto> UpdateStatusAsync(int id, string status)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) throw new NotFoundException("Cita", id);

            appointment.Status = status;

            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        // Eliminacion fisica de admin
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Appointments.DeleteAsync(id);
            if (!result) throw new NotFoundException("Cita", id);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}