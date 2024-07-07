using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.FormasPagamento;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        public async Task<IActionResult> RetornaFormasPagamento()
        {
            return Ok(_mapper.Map<IEnumerable<ReadFormaPagamentoDto>>(await _context.FormasPgtoDB.ToListAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaFormasPagamentoPorId(int id)
        {
            var formasPgto = _mapper.Map<ReadFormaPagamentoDto>(await _context.FormasPgtoDB.SingleOrDefaultAsync(f => f.Id == id));
            if(formasPgto != null)
                return Ok(formasPgto);
            return NotFound();
        }       

        [HttpPost]
        public async Task<IActionResult> AdicionaFormasPagamento([FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            FormasPagamento formasPagamento = _mapper.Map<FormasPagamento>(formasPagamentoDto);
            _context.FormasPgtoDB.Add(formasPagamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaFormasPagamentoPorId), new { Id = formasPagamento.Id }, formasPagamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaFormasPagamento(int id, [FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            var formasPgtoAntigo = await _context.FormasPgtoDB.SingleOrDefaultAsync(f => f.Id == id);
            if (formasPgtoAntigo == null)
                return NotFound();
            _mapper.Map(formasPgtoAntigo, formasPagamentoDto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFormaPagamento(int id)
        {
            var formaPagamento = await _context.FormasPgtoDB.SingleOrDefaultAsync(f => f.Id == id);
            if (formaPagamento == null)
                return NotFound();
            _context.FormasPgtoDB.Remove(formaPagamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
