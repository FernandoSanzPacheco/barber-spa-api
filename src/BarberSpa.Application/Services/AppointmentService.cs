using AutoMapper;
using BarberSpa.Application.DTOs.Appointment;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Exceptions;
using BarberSpa.Domain.Ports.Out;
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

        public async Task<AppointmentDto> CreateAsync(int userId, CreateAppointmentDto dto)
        {
            // Validar fecha futura
            if (dto.AppointmentDate <= DateTime.Now)
                throw new BusinessRuleException("InvalidDate", "La fecha debe ser futura.");

            // Mapear DTO a Entidad
            var appointment = _mapper.Map<Appointment>(dto);
            appointment.UserId = userId; // Asignar el usuario logueado

            // Guardar
            await _unitOfWork.Appointments.CreateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            // Retornar DTO con datos completos (incluyendo nombres)
            //por simplicidad retornamos lo básico o usamos GetById.
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetMyAppointmentsAsync(int userId)
        {
            var appointments = await _unitOfWork.Appointments.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<bool> CancelAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) throw new NotFoundException("Cita", id);

            if (!appointment.CanBeCancelled())
                throw new BusinessRuleException("LateCancellation", "No se puede cancelar citas pasadas o ya canceladas.");

            appointment.Status = "Cancelled";
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}