using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ApiMinhasFinancas.Services;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DocumentoController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        private readonly UsuarioService _usuarioService;
        public DocumentoController(MinhasFinancasContext context, IMapper mapper, UsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReadDocumentosDto>> RetornaDocumentos()        
        {
            return _mapper.Map<List<ReadDocumentosDto>>
                (await _context.DocumentosDB
                .Where(d=> d.UsuarioId == _usuarioService.GetUserId())
                .ToListAsync());                      
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaDocumentoPorId(int id)
        {
            var documento = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(d => d.Id == id);
            if(documento != null)
            {
                ReadDocumentosDto documentosDto = _mapper.Map<ReadDocumentosDto>(documento);
                return Ok(documentosDto);
            }                
            return NotFound();
        }

        [HttpGet("PorPeriodo")]
        public async Task<IEnumerable<ReadDocumentosDto>> ObterDocumentosPorPeriodo(   
            [FromQuery(Name = "tipo")]
            [Required] int tipo,
            [FromQuery(Name = "status")]
            [Required] char status,
            [FromQuery(Name = "dataIni")]
            [Required] DateTime dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTime dataFim)            
        {
            var documentos =  await _context.DocumentosDB
                                     .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                                     .Where(d => d.DataDocumento >= dataIni.Date && d.DataDocumento <= dataFim.Date && d.TipoConta.Tipo == tipo && d.Status == status.ToString())
                                     .ToListAsync();
            return _mapper.Map<List<ReadDocumentosDto>>(documentos);            
        }                            

        [HttpGet("ValoresPorPeriodo")]
        public async Task<ActionResult<IEnumerable<ReadTipoContaTotalDocs>>> ObterValoresPorPeriodo(
            [FromQuery(Name = "tipo")]   
            [Required] int tipo,
            [FromQuery(Name = "status")] 
            [Required] char status,
            [FromQuery(Name = "dataIni")]
            [Required] DateTime dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTime dataFim)
        {
            var totalValores = await _context.DocumentosDB
                              .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                              .Where(d => d.DataDocumento >= dataIni.Date)
                              .Where(d => d.DataDocumento <= dataFim.Date)
                              .Where(d => d.TipoConta.Tipo == tipo)
                              .Where(d => d.Status == status.ToString())
                              .Where(d => d.Valor > 0)                              
                              .GroupBy(d => new { d.TipoConta.Id, d.TipoConta.NomeConta, d.TipoConta.Tipo })
                              .Select(Valores => new ReadTipoContaTotalDocs
                              {
                                  Id = Valores.Key.Id,
                                  NomeConta = Valores.Key.NomeConta,
                                  Tipo = Valores.Key.Tipo,
                                  ValorTotal = Valores.Sum(d => d.Valor)
                              }).OrderBy(d => d.ValorTotal)
                                .ToListAsync();                             
            return Ok(totalValores);
        }

        [HttpGet("RelatorioDetalhadoTipoContas")]
        public async Task<ActionResult<IEnumerable<ReadTipoContaTotalDocs>>> RelatorioDetalhadoTipoContas(
            [FromQuery(Name = "id")]
            [Required] int id,
            [FromQuery(Name = "status")]
            [Required] char status,
            [FromQuery(Name = "dataIni")]
            [Required] DateTime dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTime dataFim)
        {
            var documentosPorTipoConta = await _context.DocumentosDB
                              .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                              .Where(d => d.DataDocumento >= dataIni.Date)
                              .Where(d => d.DataDocumento <= dataFim.Date)
                              .Where(d => d.TipoConta.Id == id)
                              .Where(d => d.Status == status.ToString())
                              .Where(d => d.Valor > 0)
                              .Select(Valores => new ReadTipoContaTotalDocs
                              {
                                  Id = Valores.TipoContaId,
                                  NomeConta = Valores.TipoConta.NomeConta,
                                  Tipo = Valores.TipoConta.Tipo,
                                  ValorTotal = Valores.Valor,
                                  DataDocumento = Valores.DataDocumento,
                                  Descricao = Valores.Descricao,
                              })
                              .OrderBy(d => d.DataDocumento)
                              .ToListAsync();
            return Ok(documentosPorTipoConta);
        }

        [HttpGet("SaldoFormasPagamento")]
        public async Task<ActionResult<IEnumerable<ReadSaldoFormasPagamentoDto>>> ObterSaldoFormasPagamento()
        {
            var saldos = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .Where(d => d.DataDocumento <= DateTime.UtcNow)
                .GroupBy(d => new { d.FormaPagamento.Id, d.FormaPagamento.Nome })                
                .Select(SaldoFormaPagamento => new ReadSaldoFormasPagamentoDto
                 {
                     Id = SaldoFormaPagamento.Key.Id,
                     Nome = SaldoFormaPagamento.Key.Nome,
                     Valor = SaldoFormaPagamento.Sum(d => d.TipoConta.Tipo == 1 && d.Status == "E" ? d.Valor : (d.TipoConta.Tipo == 2 && d.Status == "P" ? -d.Valor : 0))
                 }).ToListAsync();

            return Ok(saldos);
        }
        [HttpGet("SaldoFormasPagamento/{id}")]
        public async Task<ActionResult<IEnumerable<ReadSaldoFormasPagamentoDto>>> ObterSaldoFormaPagamentoPorId(int id)
        {
            var saldos = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .Where(d => d.DataDocumento <= DateTime.UtcNow)
                .Where(d => d.FormaPagamentoId == id)
                .SumAsync(d => d.TipoConta.Tipo == 1 && d.Status == "E" ? d.Valor : (d.TipoConta.Tipo == 2 && d.Status == "P" ? -d.Valor : 0));
            return Ok(saldos);
        }

        [HttpGet("SaldoGeral")]
        public async Task<ActionResult<double>> ObterSaldoGeral()
        {            
            var saldoEntrada = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .Where(d => d.DataDocumento <= DateTime.UtcNow && d.TipoConta.Tipo == 1)
                .SumAsync(d => d.Valor);

            var saldoSaida = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .Where(d => d.DataDocumento <= DateTime.UtcNow && d.TipoConta.Tipo == 2 && d.Status == "P")
                .SumAsync(d => d.Valor);                     
            return Ok(saldoEntrada - saldoSaida);            
        }

        [HttpGet("SaldoUltimosMeses/tipo={tipo}&qtdUltimosMeses={qtdUltimosMeses}")]
        public async Task<ActionResult<IEnumerable<ReadMesTotalDto>>> GetSaldoUltimosMeses(int tipo, int qtdUltimosMeses)
        {           
            var dataInicial = DateTime.UtcNow.AddMonths(-qtdUltimosMeses);
          
            var dataAtual = DateTime.UtcNow;
          
            var saldosPorMes = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .Where(d => d.DataDocumento >= dataInicial && d.DataDocumento <= dataAtual && d.TipoConta.Tipo == tipo)
                .GroupBy(d => new { d.DataDocumento.Year, d.DataDocumento.Month })
                .Select(g => new ReadMesTotalDto
                {
                    NumeroMes = g.Key.Month,
                    Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Total = g.Sum(d => d.Valor)
                })
                .ToListAsync();

            return Ok(saldosPorMes);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaDocumento([FromBody] UpdateDocumentosDto updateDocumentosDto)
        {
            updateDocumentosDto.UsuarioId = _usuarioService.GetUserId();
            var documento = _mapper.Map<Documentos>(updateDocumentosDto);
            _context.DocumentosDB.Add(documento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaDocumentoPorId), new { Id = documento.Id }, updateDocumentosDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaDocumento(int id, [FromBody] UpdateDocumentosDto updateDocumentosDto)
        {
            updateDocumentosDto.UsuarioId = _usuarioService.GetUserId();
            var documento = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(d => d.Id == id);
            if (documento == null)
                return NotFound();
            _mapper.Map(updateDocumentosDto, documento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaDocumento(int id)
        {
            var documento = await _context.DocumentosDB
                .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                .SingleOrDefaultAsync(d => d.Id == id);
            if (documento == null)
                return NotFound();
            _context.DocumentosDB.Remove(documento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
