using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Dtos.TipoContas;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<ReadTipoContaDto> RetornaTipoContas()
        {
            return _mapper.Map<List<ReadTipoContaDto>>(_context.TipoContasDB.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RetornaTipoContasPorId(int id)
        {
            var tipoConta = _context.TipoContasDB.SingleOrDefault(t => t.Id == id);
            if(tipoConta != null)
                return Ok(tipoConta);
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaTipoConta([FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            TipoContas tipoContas = _mapper.Map<TipoContas>(updateTipoContasDto);
            _context.TipoContasDB.Add(tipoContas);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RetornaTipoContasPorId), new { Id = tipoContas.Id }, tipoContas);
        }

        [HttpPut("{id}")]
        public IActionResult EditaTipoConta(int id, [FromBody] UpdateTipoContasDto updateTipoContasDto)
        {
            TipoContas tipoContas = _context.TipoContasDB.SingleOrDefault(t => t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _mapper.Map(updateTipoContasDto, tipoContas);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaTipoConta(int id)
        {
            var tipoContas = _context.TipoContasDB.SingleOrDefault(t=> t.Id == id);
            if (tipoContas == null)
                return NotFound();
            _context.TipoContasDB.Remove(tipoContas);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
