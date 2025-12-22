using System.ComponentModel.DataAnnotations;

namespace BarberSpa.Application.DTOs.Service
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Duration { get; set; } // Minutos
        public string ImageUrl { get; set; } = string.Empty;
    }
}