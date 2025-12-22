using System;

namespace BarberSpa.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Duration { get; set; } // En minutos
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}