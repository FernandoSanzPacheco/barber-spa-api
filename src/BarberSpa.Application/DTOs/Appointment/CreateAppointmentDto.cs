using System;

namespace BarberSpa.Application.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }
    }
}