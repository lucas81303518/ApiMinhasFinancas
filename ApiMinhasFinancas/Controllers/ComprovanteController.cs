using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Comprovantes;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ComprovanteController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public ComprovanteController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("documentos/{idDocumento}")]
        public async Task<IEnumerable<ReadComprovanteDto>> RetornaComprovantes(int idDocumento)
        {
            var comprovantes = await _context.ComprovantesDB.Where(d => d.DocumentoId == idDocumento).ToListAsync();
            return _mapper.Map<IEnumerable<ReadComprovanteDto>>(comprovantes);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> RetornaComprovantePorId(int id)
        {
            var comprovante = await _context.ComprovantesDB.SingleOrDefaultAsync(c => c.Id == id);
            if(comprovante != null)
            {
                ReadComprovanteDto readComprovanteDto = _mapper.Map<ReadComprovanteDto>(comprovante);
                return Ok(readComprovanteDto);
            }                              
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaComprovante([FromBody] UpdateComprovantesDto comprovanteDto)
        {
            var comprovante = _mapper.Map<Comprovantes>(comprovanteDto);
            _context.ComprovantesDB.Add(comprovante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaComprovantePorId), new { Id = comprovante.Id }, comprovante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarComprovante(int id, [FromBody] UpdateComprovantesDto comprovanteDto)
        {
            var comprovanteAntigo = await _context.ComprovantesDB.SingleOrDefaultAsync(c => c.Id == id);
            if(comprovanteAntigo == null)            
                return NotFound();
            _mapper.Map(comprovanteDto, comprovanteAntigo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaComprovante(int id)
        {
            var comprovanteAntigo = await _context.ComprovantesDB.SingleOrDefaultAsync(c => c.Id == id);
            if (comprovanteAntigo == null)
                return NotFound();
            _context.ComprovantesDB.Remove(comprovanteAntigo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
