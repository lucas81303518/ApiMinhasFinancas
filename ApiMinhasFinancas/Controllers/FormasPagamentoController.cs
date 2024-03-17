using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Dtos.FormasPagamento;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormasPagamentoController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public FormasPagamentoController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult RetornaFormasPagamento()
        {
            return Ok(_context.FormasPgtoDB);
        }

        [HttpGet("{id}")]
        public IActionResult RetornaFormasPagamentoPorId(int id)
        {
            var formasPgto = _context.FormasPgtoDB.SingleOrDefault(f => f.Id == id);
            if(formasPgto != null)
                return Ok(formasPgto);
            return NotFound();
        }       

        [HttpPost]
        public IActionResult AdicionaFormasPagamento([FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            FormasPagamento formasPagamento = _mapper.Map<FormasPagamento>(formasPagamentoDto);
            _context.FormasPgtoDB.Add(formasPagamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RetornaFormasPagamentoPorId), new { Id = formasPagamento.Id }, formasPagamento);
        }

        [HttpPut("{id}")]
        public IActionResult EditaFormasPagamento(int id, [FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            var formasPgtoAntigo = _context.FormasPgtoDB.SingleOrDefault(f => f.Id == id);
            if (formasPgtoAntigo == null)
                return NotFound();
            _mapper.Map(formasPgtoAntigo, formasPagamentoDto);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFormaPagamento(int id)
        {
            var formaPagamento = _context.FormasPgtoDB.SingleOrDefault(f => f.Id == id);
            if (formaPagamento == null)
                return NotFound();
            _context.FormasPgtoDB.Remove(formaPagamento);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
