using BarberSpa.Application.DTOs.Service;
using BarberSpa.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //Solo usuarios logueados (idealmente solo Admins)
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        [AllowAnonymous] // cualquiera puede ver catalogo
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
        {
            return Ok(await _serviceService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            return Ok(await _serviceService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> Create(CreateServiceDto dto)
        {
            var created = await _serviceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDto>> Update(int id, CreateServiceDto dto)
        {
            return Ok(await _serviceService.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            return NoContent();
        }
    }
}