using BarberSpa.Application.DTOs.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> GetByIdAsync(int id);
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);
        Task<ServiceDto> UpdateAsync(int id, CreateServiceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}