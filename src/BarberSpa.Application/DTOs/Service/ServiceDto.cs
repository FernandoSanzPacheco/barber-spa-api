namespace BarberSpa.Application.DTOs.Service
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}