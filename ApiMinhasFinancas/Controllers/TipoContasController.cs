using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Dtos.TipoContas;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TipoContasController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public TipoContasController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReadTipoContaDto>> RetornaTipoContas()
        {
            return _mapper.Map<List<ReadTipoContaDto>>(await _context.TipoContasDB.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaTipoContasPorId(int id)
        {
            var tipoConta = await _context.TipoContasDB.SingleOrDefaultAsync(t => t.Id == id);
            if(tipoConta != null)
                return Ok(tipoConta);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaTipoConta([FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            TipoContas tipoContas = _mapper.Map<TipoContas>(updateTipoContasDto);
            _context.TipoContasDB.Add(tipoContas);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaTipoContasPorId), new { Id = tipoContas.Id }, tipoContas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaTipoConta(int id, [FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            var tipoContas = await _context.TipoContasDB.SingleOrDefaultAsync(t => t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _mapper.Map(updateTipoContasDto, tipoContas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaTipoConta(int id)
        {
            var tipoContas = await _context.TipoContasDB.SingleOrDefaultAsync(t=> t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _context.TipoContasDB.Remove(tipoContas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
