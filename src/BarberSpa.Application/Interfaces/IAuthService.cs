using BarberSpa.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace BarberSpa.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}