using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.FormasPagamento;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMinhasFinancas.Services;

namespace BibliotecaMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FormasPagamentoController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        private UsuarioService _usuarioService;

        public FormasPagamentoController(MinhasFinancasContext context, IMapper mapper, UsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> RetornaFormasPagamento()
        {
            return Ok(_mapper.Map<IEnumerable<ReadFormaPagamentoDto>>
            (await _context.FormasPgtoDB
                .Where(f => f.UsuarioId == _usuarioService.GetUserId())
                .ToListAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaFormasPagamentoPorId(int id)
        {
            var formasPgto = _mapper.Map<ReadFormaPagamentoDto>
                (await _context.FormasPgtoDB
                .Where(f => f.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(f => f.Id == id));
            if(formasPgto != null)
                return Ok(formasPgto);
            return NotFound();
        }       

        [HttpPost]
        public async Task<IActionResult> AdicionaFormasPagamento([FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            formasPagamentoDto.UsuarioId = _usuarioService.GetUserId();
            FormasPagamento formasPagamento = _mapper.Map<FormasPagamento>(formasPagamentoDto);
            _context.FormasPgtoDB.Add(formasPagamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaFormasPagamentoPorId), new { Id = formasPagamento.Id }, formasPagamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaFormasPagamento(int id, [FromBody] UpdateFormasPagamentoDto formasPagamentoDto)
        {
            formasPagamentoDto.UsuarioId = _usuarioService.GetUserId();
            var formasPgtoAntigo = await _context.FormasPgtoDB
                .Where(f=> f.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(f => f.Id == id);
            if (formasPgtoAntigo == null)
                return NotFound();
            _mapper.Map(formasPgtoAntigo, formasPagamentoDto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFormaPagamento(int id)
        {
            var formaPagamento = await _context.FormasPgtoDB
                .Where(f => f.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(f => f.Id == id);
            if (formaPagamento == null)
                return NotFound();
            _context.FormasPgtoDB.Remove(formaPagamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
