using BarberSpa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberSpa.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            // Nombre de la tabla en BD
            builder.ToTable("Services");

            // Clave primaria
            builder.HasKey(s => s.Id);

            // Propiedades
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(500); // Mas espacio para describir el servicio

            // 10 dígitos en total, con 2 decimales.
            builder.Property(s => s.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(s => s.Duration)
                .IsRequired();

            builder.Property(s => s.ImageUrl)
                .HasMaxLength(250);

            builder.Property(s => s.IsActive)
                .IsRequired();
        }
    }
}