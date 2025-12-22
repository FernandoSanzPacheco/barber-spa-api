using BarberSpa.Application.DTOs.Appointment;
using FluentValidation;
using System;

namespace BarberSpa.Application.Validators
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(x => x.BarberId).GreaterThan(0).WithMessage("Debes seleccionar un barbero válido.");
            RuleFor(x => x.ServiceId).GreaterThan(0).WithMessage("Debes seleccionar un servicio válido.");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de la cita debe ser en el futuro.");
        }
    }
}