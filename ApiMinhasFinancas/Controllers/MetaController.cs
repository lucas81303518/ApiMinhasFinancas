using BibliotecaMinhasFinancas.Data.Dtos.Metas;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMinhasFinancas.Dtos.Metas;
using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services;
using BibliotecaMinhasFinancas.Data.Dtos.MovimentacaoMetas;
using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;

namespace BibliotecaMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UsuarioAtivo")]
    public class MetaController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        private readonly UsuarioService _usuarioService;
        private readonly MovimentacaoMetasService _metasService;
        private FinanceiroService _financeiroService;
        private readonly SaldoMensalService _saldoMensalService;
        public MetaController(MinhasFinancasContext context, IMapper mapper,
                              UsuarioService usuarioService, 
                              MovimentacaoMetasService metasService, FinanceiroService financeiroService, SaldoMensalService saldoMensalService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _metasService = metasService;
            _financeiroService = financeiroService;
            _saldoMensalService = saldoMensalService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReadMetasDto>> ObterMetas()
        {
            var metas= _mapper.Map<List<ReadMetasDto>>
                (await _context.MetasDB
                .Where(m=> m.UsuarioId == _usuarioService.GetUserId())
                .ToListAsync());          
            return metas;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterMetaPorId(int id)
        {
            var meta = await _context.MetasDB
                .Where(m => m.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(m => m.Id == id);
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
            updateMetasDto.UsuarioId = _usuarioService.GetUserId();
            var meta = _mapper.Map<Metas>(updateMetasDto);
            _context.MetasDB.Add(meta);    
            await _context.SaveChangesAsync();
            return Created(nameof(ObterMetaPorId), new { id = meta.Id});
        }

        /* Desabilitada por enquanto...
        [HttpPut("{id}")]
        public async Task<IActionResult> EditaMeta(int id, [FromBody] UpdateMetasDto updateMetasDto)
        {
            updateMetasDto.UsuarioId = _usuarioService.GetUserId();
            var meta = await _context.MetasDB
                .Where(m => m.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();

            await _metasService.InsereMovimentacao(
                new UpdateMovimentacaoMetas
                {
                    DataHora = DateTime.UtcNow,
                    Descricao = $"Meta código {id} alterada.",
                    MetaId = id,
                    TipoOperacao = TipoMovimentacaoMetas.AlteracaoMeta,
                    UsuarioId = _usuarioService.GetUserId(),
                    Valor = updateMetasDto.ValorObjetivo
                });

            _mapper.Map(updateMetasDto, meta);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        [HttpPut("{id}/SomarSaldo")]        
        public async Task<IActionResult> SomarSaldoMeta(int id, [FromBody] UpdateSaldoMeta updateSaldoMeta)
        {            
            var meta = await _context.MetasDB
                .Where(m => m.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();
            meta.ValorResultado += updateSaldoMeta.Valor;

            await _metasService.InsereMovimentacao(
                new UpdateMovimentacaoMetas
                {
                    DataHora = DateTime.UtcNow,
                    Descricao = $"Saldo {updateSaldoMeta.Valor.ToString("C2")} acrescentado",
                    MetaId = id,
                    TipoOperacao = TipoMovimentacaoMetas.SomarSaldo,
                    UsuarioId = _usuarioService.GetUserId(),
                    Valor = updateSaldoMeta.Valor
                });

            await _saldoMensalService.CriarOuAtualizarSaldoAsync
            (new SaldoDto
            {
                TipoOperacao = TipoOperacao.Inserir,
                TipoDocumento = TipoDocumento.Saida,
                ValorDocumento = updateSaldoMeta.Valor
            });            

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/SubtrairSaldo")]
        public async Task<IActionResult> SubtrairSaldoMeta(int id, [FromBody] UpdateSaldoMeta updateSaldoMeta)
        {
            var meta = await _context.MetasDB
                .Where(m => m.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();
            meta.ValorResultado -= updateSaldoMeta.Valor;

            await _metasService.InsereMovimentacao(
                new UpdateMovimentacaoMetas
                {
                    DataHora = DateTime.UtcNow,
                    Descricao = $"Saldo {updateSaldoMeta.Valor.ToString("C2")} subtraído",
                    MetaId = id,
                    TipoOperacao = TipoMovimentacaoMetas.SubtrairSaldo,
                    UsuarioId = _usuarioService.GetUserId(),
                    Valor = updateSaldoMeta.Valor
                });

            await _saldoMensalService.CriarOuAtualizarSaldoAsync
            (new SaldoDto
            {
                TipoOperacao = TipoOperacao.Inserir,
                TipoDocumento = TipoDocumento.Entrada,
                ValorDocumento = updateSaldoMeta.Valor
            });           

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaMeta(int id)
        {
            var meta = await _context.MetasDB
                .Where(m => m.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meta == null)
                return NotFound();
            _context.MetasDB.Remove(meta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
