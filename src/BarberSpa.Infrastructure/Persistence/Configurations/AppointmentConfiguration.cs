using BarberSpa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberSpa.Infrastructure.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(a => a.Id);

            // Relación con User
            builder.HasOne(a => a.User)
                   .WithMany() // Un usuario tiene muchas citas
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // No borrar citas si se borra el usuario

            // Relación con Barber
            builder.HasOne(a => a.Barber)
                   .WithMany(b => b.Appointments)
                   .HasForeignKey(a => a.BarberId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Service
            builder.HasOne(a => a.Service)
                   .WithMany()
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}