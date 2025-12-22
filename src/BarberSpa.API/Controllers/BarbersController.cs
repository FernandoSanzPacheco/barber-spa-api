using BarberSpa.Application.DTOs.Barber;
using BarberSpa.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        // POST: solo logueados
        [HttpPost]
        public async Task<ActionResult<BarberDto>> Create(CreateBarberDto dto)
        {
            var created = await _barberService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: solo logueados
        [HttpPut("{id}")]
        public async Task<ActionResult<BarberDto>> Update(int id, CreateBarberDto dto)
        {
            return Ok(await _barberService.UpdateAsync(id, dto));
        }

        // DELETE: solo logueados
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _barberService.DeleteAsync(id);
            return NoContent();
        }
    }
}