using System;

namespace BarberSpa.Application.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }

        // Datos aplanados para facilitar la vista en React
        public string BarberName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ClientName { get; set; } = string.Empty;
    }
}