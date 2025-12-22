using BarberSpa.Application.DTOs.Auth;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Entities;
using BarberSpa.Domain.Exceptions;
using BarberSpa.Domain.Ports.Out;
using Microsoft.Extensions.Configuration; // Necesario para leer JWT_SECRET
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace BarberSpa.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            // Leemos las variables de entorno cargadas en memoria
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "SecretKeyDefault_CHANGE_ME";
            _jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "BarberSpa";
            _jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "BarberSpaClient";
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // Verificar si existe
            if (await _unitOfWork.Users.ExistsByEmailAsync(dto.Email))
                throw new BusinessRuleException("EmailExists", "El correo ya está registrado.");

            // Crear usuario con contraseña encriptada
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // Encriptación
                Role = "Client",
                IsActive = true
            };

            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Generar token y responder
            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.GetFullName(),
                Role = user.Role,
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

            // Validar usuario y contraseña
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new BusinessRuleException("InvalidCredentials", "Correo o contraseña incorrectos.");

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.GetFullName(),
                Role = user.Role,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtAudience,
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}