using BibliotecaMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Metas;
using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Data.Dtos.Metas;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMinhasFinancas.Dtos.Metas;
using ApiMinhasFinancas.Data;

namespace BibliotecaMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MetaController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public MetaController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReadMetasDto>> ObterMetas()
        {
            return _mapper.Map<List<ReadMetasDto>>(await _context.MetasDB.ToListAsync());          
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterMetaPorId(int id)
        {
            var meta = await _context.MetasDB.SingleOrDefaultAsync(m => m.Id == id);
            if (meta != null)
            {
                ReadMetasDto readMeta = _mapper.Map<ReadMetasDto>(meta);
                return Ok(readMeta);
            }                
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaMeta([FromBody] UpdateMetasDto updateMetasDto)
        {
            var meta = _mapper.Map<Metas>(updateMetasDto);
            _context.MetasDB.Add(meta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterMetaPorId), new { id = meta.Id, meta });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaMeta(int id, [FromBody] UpdateMetasDto updateMetasDto)
        {
            var meta = await _context.MetasDB.SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();
            _mapper.Map(updateMetasDto, meta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaMeta(int id)
        {
            var meta = await _context.MetasDB.SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();
            _context.MetasDB.Remove(meta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
