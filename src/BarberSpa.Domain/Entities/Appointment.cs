using System;

namespace BarberSpa.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; } // Fecha y Hora combinadas
        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relación con el Cliente (User)
        public int UserId { get; set; }
        public User? User { get; set; }

        // Relación con el Barbero
        public int BarberId { get; set; }
        public Barber? Barber { get; set; }

        // Relación con el Servicio
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        // Lógica de dominio
        public bool IsFutureAppointment() => AppointmentDate > DateTime.Now;
        public bool CanBeCancelled() => Status == "Scheduled" && IsFutureAppointment();
    }
}
