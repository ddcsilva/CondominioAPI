using CondominioAPI.Domain.Entities;
using CondominioAPI.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CondominioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondominioController : ControllerBase
    {
        private readonly ICondominioRepository _repository;

        public CondominioController(ICondominioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Obter todos os condomínios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Condominio>>> GetCondominios()
        {
            var condominios = await _repository.GetAllAsync();
            return Ok(condominios);
        }

        // Obter um condomínio específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Condominio>> GetCondominio(Guid id)
        {
            var condominio = await _repository.GetByIdAsync(id);

            if (condominio == null)
            {
                return NotFound();
            }

            return Ok(condominio);
        }

        // Criar um novo condomínio
        [HttpPost]
        public async Task<ActionResult<Condominio>> CreateCondominio(Condominio condominio)
        {
            await _repository.AddAsync(condominio);
            return CreatedAtAction(nameof(GetCondominio), new { id = condominio.Id }, condominio);
        }

        // Atualizar um condomínio existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCondominio(Guid id, Condominio condominio)
        {
            if (id != condominio.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(condominio);
            return NoContent();
        }

        // Excluir um condomínio pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondominio(Guid id)
        {
            var condominio = await _repository.GetByIdAsync(id);

            if (condominio == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

