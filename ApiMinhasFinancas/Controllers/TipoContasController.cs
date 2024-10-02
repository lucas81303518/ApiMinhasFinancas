using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.TipoContas;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMinhasFinancas.Services;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UsuarioAtivo")]
    public class TipoContasController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        private UsuarioService _usuarioService;

        public TipoContasController(MinhasFinancasContext context, IMapper mapper, UsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReadTipoContaDto>> RetornaTipoContas()
        {
            return _mapper.Map<List<ReadTipoContaDto>>(await _context.TipoContasDB
                .Where(t=> t.UsuarioId == _usuarioService.GetUserId())
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaTipoContasPorId(int id)
        {
            var tipoConta = await _context.TipoContasDB
                .Where(t => t.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(t => t.Id == id);
            if(tipoConta != null)
                return Ok(tipoConta);
            return NotFound();
        }

        [HttpGet("Tipo/{id}")]
        public async Task<IActionResult> RetornaTipoContasPorTipo(int id)
        {
            var tipoConta = await _context.TipoContasDB.Where(t => t.Tipo == id)
                                                       .Where(t => t.UsuarioId == _usuarioService.GetUserId())
                                                       .ToListAsync();
            return Ok(tipoConta);           
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaTipoConta([FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            updateTipoContasDto.UsuarioId = _usuarioService.GetUserId();
            TipoContas tipoContas = _mapper.Map<TipoContas>(updateTipoContasDto);
            _context.TipoContasDB.Add(tipoContas);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaTipoContasPorId), new { Id = tipoContas.Id }, tipoContas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaTipoConta(int id, [FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            updateTipoContasDto.UsuarioId = _usuarioService.GetUserId();
            var tipoContas = await _context.TipoContasDB
                .Where(t => t.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(t => t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _mapper.Map(updateTipoContasDto, tipoContas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaTipoConta(int id)
        {
            var tipoContas = await _context.TipoContasDB
                .Where(t => t.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(t=> t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _context.TipoContasDB.Remove(tipoContas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
