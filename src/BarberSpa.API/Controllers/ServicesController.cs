using BarberSpa.Application.DTOs.Service;
using BarberSpa.Application.Interfaces;
using BarberSpa.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: Publico
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
        {
            return Ok(await _serviceService.GetAllAsync());
        }

        // GET: Publico
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            return Ok(await _serviceService.GetByIdAsync(id));
        }

        // POST: Solo Admin
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ServiceDto>> Create(CreateServiceDto dto)
        {
            var created = await _serviceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: Solo Admin
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ServiceDto>> Update(int id, CreateServiceDto dto)
        {
            return Ok(await _serviceService.UpdateAsync(id, dto));
        }

        // DELETE: Solo Admin (Eliminación fisica validada)
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            return NoContent();
        }
    }
}