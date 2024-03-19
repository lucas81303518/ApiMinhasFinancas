using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Data.Dtos.Metas;
using ApiMinhasFinancas.Dtos.Documentos;
using ApiMinhasFinancas.Dtos.Metas;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<ReadMetasDto> ObterMetas()
        {
            return _mapper.Map<List<ReadMetasDto>>(_context.MetasDB.ToList());          
        }

        [HttpGet("{id}")]
        public IActionResult ObterMetaPorId(int id)
        {
            var meta = _context.MetasDB.SingleOrDefault(m => m.Id == id);
            if (meta != null)
            {
                ReadMetasDto readMeta = _mapper.Map<ReadMetasDto>(meta);
                return Ok(readMeta);
            }                
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaMeta([FromBody] UpdateMetasDto updateMetasDto)
        {
            var meta = _mapper.Map<Metas>(updateMetasDto);
            _context.MetasDB.Add(meta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterMetaPorId), new { id = meta.Id, meta });
        }

        [HttpPut("{id}")]
        public IActionResult EditaMeta(int id, [FromBody] UpdateMetasDto updateMetasDto)
        {
            var meta = _context.MetasDB.SingleOrDefault(m => m.Id == id);
            if (meta == null)
                return NotFound();
            _mapper.Map(updateMetasDto, meta);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaMeta(int id)
        {
            var meta = _context.MetasDB.SingleOrDefault(m => m.Id == id);
            if (meta == null)
                return NotFound();
            _context.MetasDB.Remove(meta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
