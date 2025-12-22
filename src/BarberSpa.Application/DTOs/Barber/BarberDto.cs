namespace BarberSpa.Application.DTOs.Barber
{
    public class BarberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}