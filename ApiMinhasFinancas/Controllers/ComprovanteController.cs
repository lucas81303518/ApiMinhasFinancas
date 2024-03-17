using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Login;
using ApiMinhasFinancas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprovanteController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        public ComprovanteController(MinhasFinancasContext context)
        {
            _context = context;
        }       
        [HttpGet]
        public IActionResult RetornaComprovantes()
        {
            return Ok(_context.ComprovantesDB);
        }
        [HttpGet("{Id}")]
        public IActionResult RetornaComprovantePorId(int id)
        {
            var comprovante = _context.ComprovantesDB.SingleOrDefault(c => c.Id == id);
            if(comprovante != null)                
                return Ok(_context.ComprovantesDB);
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaComprovante([FromBody] Comprovantes comprovante)
        {
            _context.ComprovantesDB.Add(comprovante);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RetornaComprovantePorId), new { Id = comprovante.Id }, comprovante);
        }

        [HttpPut("{id}")]
        public IActionResult EditarComprovante(int id, [FromBody] Comprovantes comprovante)
        {
            var comprovanteAntigo = _context.ComprovantesDB.SingleOrDefault(c => c.Id == id);
            if(comprovanteAntigo == null)            
                return NotFound();
            comprovanteAntigo.Id = comprovante.Id;
            _context.Entry(comprovanteAntigo).CurrentValues.SetValues(comprovante);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaComprovante(int id)
        {
            var comprovanteAntigo = _context.ComprovantesDB.SingleOrDefault(c => c.Id == id);
            if (comprovanteAntigo == null)
                return NotFound();
            _context.ComprovantesDB.Remove(comprovanteAntigo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
