using BarberSpa.Application.DTOs.Barber;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.Application.Interfaces
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberDto>> GetAllAsync();
        Task<BarberDto> GetByIdAsync(int id);
        Task<BarberDto> CreateAsync(CreateBarberDto dto);
        Task<BarberDto> UpdateAsync(int id, CreateBarberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
