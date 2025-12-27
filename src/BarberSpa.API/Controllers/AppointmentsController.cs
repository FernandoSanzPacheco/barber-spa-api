using BarberSpa.Application.DTOs.Appointment;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // todos los endpoints requieren estar logueado
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // Crear Cita (Cliente para si mismo)
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(CreateAppointmentDto dto)
        {
            // Obtenemos el ID del usuario desde el Token
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var appointment = await _appointmentService.CreateAsync(userId, dto);

            return Ok(appointment);
        }

        // Obtener mis citas (client)
        [HttpGet("my-appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetMine()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var appointments = await _appointmentService.GetMyAppointmentsAsync(userId);
            return Ok(appointments);
        }

        // Obtener todas las citas (admin y recepcionist)
        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Receptionist)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            return Ok(await _appointmentService.GetAllAsync());
        }

        // Cancelar Cita
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            // Obtenemos el rol del usuario actual para pasarlo al servicio
            var role = User.FindFirst(ClaimTypes.Role)!.Value;

            await _appointmentService.CancelAsync(id, userId, role);
            return NoContent();
        }

        // Actualizar Estado (como "completed") Solo Admin y Recepcionista
        [HttpPut("{id}/status")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Receptionist)]
        public async Task<ActionResult<AppointmentDto>> UpdateStatus(int id, [FromBody] string status)
        {
            return Ok(await _appointmentService.UpdateStatusAsync(id, status));
        }

        // Eliminar Cita Fisicamente - Admin
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}