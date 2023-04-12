using CondominioAPI.Domain.Entities;
using CondominioAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CondominioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondominioController : ControllerBase
    {
        private readonly ICondominioService _service;

        public CondominioController(ICondominioService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // Obter todos os condomínios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Condominio>>> GetCondominios()
        {
            var condominios = await _service.GetAllAsync();
            return Ok(condominios);
        }

        // Obter um condomínio específico pelo ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<Condominio>> GetCondominio(Guid id)
        {
            var condominio = await _service.GetByIdAsync(id);

            if (condominio == null)
            {
                return NotFound();
            }

            return Ok(condominio);
        }

        // Criar um novo condomínio
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<Condominio>> CreateCondominio(Condominio condominio)
        {
            await _service.AddAsync(condominio);
            return CreatedAtAction(nameof(GetCondominio), new { id = condominio.Id }, condominio);
        }

        // Atualizar um condomínio existente
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateCondominio(Guid id, Condominio condominio)
        {
            if (id != condominio.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(condominio);
            return NoContent();
        }

        // Excluir um condomínio pelo ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteCondominio(Guid id)
        {
            var condominio = await _service.GetByIdAsync(id);

            if (condominio == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
