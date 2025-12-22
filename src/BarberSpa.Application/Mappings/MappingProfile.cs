using AutoMapper;
using BarberSpa.Application.DTOs.Barber;
using BarberSpa.Application.DTOs.Service;
using BarberSpa.Application.DTOs.Appointment;
using BarberSpa.Domain.Entities;

namespace BarberSpa.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Barbero
            CreateMap<Barber, BarberDto>();
            CreateMap<CreateBarberDto, Barber>();

            // Servicio
            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>();

            // Citas (para traer nombres de relaciones)
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.BarberName, opt => opt.MapFrom(src => src.Barber != null ? src.Barber.Name : string.Empty))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : string.Empty))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service != null ? src.Service.Price : 0))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.User != null ? src.User.GetFullName() : string.Empty));

            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Scheduled"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}