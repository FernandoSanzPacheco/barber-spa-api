using System;
using System.Collections.Generic;

namespace BarberSpa.Domain.Entities
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty; // Ej: Corte Clásico, Barba
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relación: Un barbero tiene muchas citas
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
