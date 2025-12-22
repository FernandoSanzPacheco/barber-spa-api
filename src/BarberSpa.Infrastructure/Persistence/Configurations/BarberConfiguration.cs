using BarberSpa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberSpa.Infrastructure.Persistence.Configurations
{
    public class BarberConfiguration : IEntityTypeConfiguration<Barber>
    {
        public void Configure(EntityTypeBuilder<Barber> builder)
        {
            // Nombre de la tabla en BD
            builder.ToTable("Barbers");

            // Clave primaria
            builder.HasKey(b => b.Id);

            // Propiedades
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Specialty)
                .IsRequired()
                .HasMaxLength(100); // Ej: "Barba y Corte"

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Phone)
                .HasMaxLength(20);

            builder.Property(b => b.ImageUrl)
                .HasMaxLength(250); // URL de la foto

            builder.Property(b => b.IsActive)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();
        }
    }
}
