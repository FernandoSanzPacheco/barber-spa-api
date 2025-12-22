using AutoMapper;
using BarberSpa.Application.DTOs.Service;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Exceptions;
using BarberSpa.Domain.Ports.Out;
using System.Threading.Tasks;

namespace BarberSpa.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            // usamos GetActiveAsync para traer solo los servicios disponibles
            var services = await _unitOfWork.Services.GetActiveAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }
        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null) throw new NotFoundException("Servicio", id);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            service.IsActive = true; // Por defecto activo al crear

            await _unitOfWork.Services.CreateAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateAsync(int id, CreateServiceDto dto)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null) throw new NotFoundException("Servicio", id);

            // Actualizamos campos manualmente o usando Mapper
            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Price = dto.Price;
            service.Duration = dto.Duration;
            service.ImageUrl = dto.ImageUrl;

            await _unitOfWork.Services.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Para borrado logico, cambiaríamos IsActive = false.
            var deleted = await _unitOfWork.Services.DeleteAsync(id);
            if (!deleted) throw new NotFoundException("Servicio", id);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}