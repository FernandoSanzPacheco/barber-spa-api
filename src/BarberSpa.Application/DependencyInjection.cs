using BarberSpa.Application.DTOs.Auth;
using BarberSpa.Application.Interfaces;
using BarberSpa.Application.Mappings;
using BarberSpa.Application.Services;
using BarberSpa.Application.Validators;
using FluentValidation; 
using Microsoft.Extensions.DependencyInjection;

namespace BarberSpa.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Servicios
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBarberService, BarberService>();
            services.AddScoped<IServiceService, ServiceService>();

            // Validadores (Registra todos los que estén en este proyecto automáticamente)
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

            return services;
        }
    }
}