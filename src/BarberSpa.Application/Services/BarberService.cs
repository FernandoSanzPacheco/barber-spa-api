using AutoMapper;
using BarberSpa.Application.DTOs.Barber;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Exceptions;
using BarberSpa.Domain.Ports.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BarberDto>> GetAllAsync()
        {
            // Usamos el metodo GetActiveAsync que ya tenías en el repositorio
            var barbers = await _unitOfWork.Barbers.GetActiveAsync();
            return _mapper.Map<IEnumerable<BarberDto>>(barbers);
        }

        public async Task<BarberDto> GetByIdAsync(int id)
        {
            var barber = await _unitOfWork.Barbers.GetByIdAsync(id);
            if (barber == null) throw new NotFoundException("Barbero", id);
            return _mapper.Map<BarberDto>(barber);
        }

        public async Task<BarberDto> CreateAsync(CreateBarberDto dto)
        {
            var barber = _mapper.Map<Barber>(dto);
            barber.IsActive = true;

            await _unitOfWork.Barbers.CreateAsync(barber);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BarberDto>(barber);
        }

        public async Task<BarberDto> UpdateAsync(int id, CreateBarberDto dto)
        {
            var barber = await _unitOfWork.Barbers.GetByIdAsync(id);
            if (barber == null) throw new NotFoundException("Barbero", id);

            // Actualizacion manual de campos
            barber.Name = dto.Name;
            barber.Specialty = dto.Specialty;
            barber.Email = dto.Email;
            barber.Phone = dto.Phone;
            barber.ImageUrl = dto.ImageUrl;

            await _unitOfWork.Barbers.UpdateAsync(barber);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BarberDto>(barber);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Borrado fisico
            var result = await _unitOfWork.Barbers.DeleteAsync(id);
            if (!result) throw new NotFoundException("Barbero", id);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}