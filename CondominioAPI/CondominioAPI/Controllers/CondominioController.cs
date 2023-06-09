﻿using AutoMapper;
using CondominioAPI.Application.DTOs;
using CondominioAPI.Application.Services;
using CondominioAPI.Domain.Entities;
using CondominioAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CondominioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondominioController : ControllerBase
    {
        private readonly ICondominioService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CondominioController> _logger;

        public CondominioController(ICondominioService service, IMapper mapper, ILogger<CondominioController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetCondominios()
        {
            _logger.LogInformation("Iniciando a busca por todos os condomínios.");
            var condominios = await _service.GetAllAsync();
            var condominiosDTO = _mapper.Map<IEnumerable<CondominioDTO>>(condominios);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Busca realizada com sucesso", condominiosDTO));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCondominioById(Guid id)
        {
            _logger.LogInformation($"Buscando condomínio por ID: {id}.");
            var condominio = await _service.GetByIdAsync(id);
            if (condominio == null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Condomínio não encontrado", null));
            }

            var condominioDTO = _mapper.Map<CondominioDTO>(condominio);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Busca realizada com sucesso", condominioDTO));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCondominio([FromBody] CondominioDTO condominioDTO)
        {
            _logger.LogInformation("Criando um novo condomínio.");

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Dados de entrada inválidos", ModelState));
            }

            var condominio = _mapper.Map<Condominio>(condominioDTO);
            var createdCondominio = await _service.AddAsync(condominio);
            var createdCondominioDTO = _mapper.Map<CondominioDTO>(createdCondominio);
            return CreatedAtAction(nameof(GetCondominioById), new { id = createdCondominioDTO.Id }, new ApiResponse(StatusCodes.Status201Created, "Condomínio criado com sucesso", createdCondominioDTO));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCondominio(Guid id, [FromBody] CondominioDTO condominioDTO)
        {
            _logger.LogInformation($"Atualizando o condomínio com ID: {id}.");
            var condominioToUpdate = await _service.GetByIdAsync(id);

            if (condominioToUpdate == null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Condomínio não encontrado", null));
            }

            var updatedCondominio = _mapper.Map<Condominio>(condominioDTO);
            updatedCondominio.Id = id;
            await _service.UpdateAsync(updatedCondominio);

            var updatedCondominioDTO = _mapper.Map<CondominioDTO>(updatedCondominio);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Condomínio atualizado com sucesso", updatedCondominioDTO));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondominio(Guid id)
        {
            _logger.LogInformation($"Excluindo o condomínio com ID: {id}.");
            var condominioToDelete = await _service.GetByIdAsync(id);
            if (condominioToDelete == null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Condomínio não encontrado", null));
            }

            await _service.DeleteAsync(condominioToDelete.Id);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Condomínio excluído com sucesso", null));
        }
    }
}