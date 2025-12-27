using BarberSpa.Application.DTOs.Barber;
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
    public class BarbersController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarbersController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        // GET: Publico, cualquiera puede ver la lista
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BarberDto>>> GetAll()
        {
            return Ok(await _barberService.GetAllAsync());
        }

        // GET: Publico, cualquiera puede ver el detalle
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BarberDto>> GetById(int id)
        {
            return Ok(await _barberService.GetByIdAsync(id));
        }

        // POST: Solo Admin puede crear barberos
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<BarberDto>> Create(CreateBarberDto dto)
        {
            var created = await _barberService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: Solo Admin puede editar
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<BarberDto>> Update(int id, CreateBarberDto dto)
        {
            return Ok(await _barberService.UpdateAsync(id, dto));
        }

        // DELETE: Solo Admin puede borrar, eliminacion fisica
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _barberService.DeleteAsync(id);
            return NoContent();
        }
    }
}