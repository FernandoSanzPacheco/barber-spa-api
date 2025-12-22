using BarberSpa.Application.DTOs.Appointment;
using BarberSpa.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Solo usuarios logueados
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(CreateAppointmentDto dto)
        {
            // Obtener ID del usuario desde el Token
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var appointment = await _appointmentService.CreateAsync(userId, dto);
            return Ok(appointment);
        }

        [HttpGet("my-appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetMine()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var appointments = await _appointmentService.GetMyAppointmentsAsync(userId);
            return Ok(appointments);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _appointmentService.CancelAsync(id);
            return NoContent();
        }
    }
}