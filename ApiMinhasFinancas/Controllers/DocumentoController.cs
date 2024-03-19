using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Dtos.Documentos;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public DocumentoController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ReadDocumentosDto> RetornaDocumentos()        
        {
            return _mapper.Map<List<ReadDocumentosDto>>(_context.DocumentosDB.ToList());                      
        }

        [HttpGet("{id}")]
        public IActionResult RetornaDocumentoPorId(int id)
        {
            var documento = _context.DocumentosDB.SingleOrDefault(d => d.Id == id);
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
            [Required] DateTimeOffset dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTimeOffset dataFim)            
        {
            var documentos =  await _context.DocumentosDB
                                     .Where(d => d.DataDocumento >= dataIni.UtcDateTime && d.DataDocumento <= dataFim.UtcDateTime && d.TipoConta.Tipo == tipo && d.Status == status.ToString())
                                     .ToListAsync();
            return _mapper.Map<List<ReadDocumentosDto>>(documentos);            
        }                            

        [HttpGet("ValoresPorPeriodo")]
        public async Task<ActionResult<IEnumerable<double>>> ObterValoresPorPeriodo(
            [FromQuery(Name = "tipo")]   
            [Required] int tipo,
            [FromQuery(Name = "status")] 
            [Required] char status,
            [FromQuery(Name = "dataIni")]
            [Required] DateTimeOffset dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTimeOffset dataFim)
        {            
            var totalValores = await _context.DocumentosDB
                              .Where(d => d.DataDocumento >= dataIni.UtcDateTime && d.DataDocumento <= dataFim.UtcDateTime && d.TipoConta.Tipo == tipo && d.Status == status.ToString())
                              .SumAsync(d => (double?)(d.Valor)) ?? 0;
            return Ok(totalValores);
        }

        [HttpGet("SaldoFormasPagamento")]
        public async Task<ActionResult<IEnumerable<ReadSaldoFormasPagamentoDto>>> ObterSaldoFormasPagamento()
        {
            var saldos = await _context.DocumentosDB
                .Where(d => d.DataDocumento <= DateTime.UtcNow)
                .GroupBy(d => new { d.FormaPagamento.Id, d.FormaPagamento.Nome })                
                .Select(SaldoFormaPagamento => new ReadSaldoFormasPagamentoDto
                 {
                     Id = SaldoFormaPagamento.Key.Id,
                     Nome = SaldoFormaPagamento.Key.Nome,
                     ValorTotal = SaldoFormaPagamento.Sum(d => d.TipoConta.Tipo == 1 && d.Status == "E" ? d.Valor : (d.TipoConta.Tipo == 2 && d.Status == "P" ? -d.Valor : 0))
                 }).ToListAsync();

            return Ok(saldos);
        }

        [HttpGet("SaldoGeral")]
        public async Task<ActionResult<double>> ObterSaldoGeral()
        {            
            var saldoEntrada = await _context.DocumentosDB
                .Where(d => d.DataDocumento <= DateTime.UtcNow && d.TipoConta.Tipo == 1)
                .SumAsync(d => d.Valor);

            var saldoSaida = await _context.DocumentosDB
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
                .Where(d => d.DataDocumento >= dataInicial && d.DataDocumento <= dataAtual && d.TipoConta.Tipo == tipo)
                .GroupBy(d => new { d.DataDocumento.Year, d.DataDocumento.Month })
                .Select(g => new ReadMesTotalDto
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Total = g.Sum(d => d.Valor)
                })
                .ToListAsync();

            return Ok(saldosPorMes);
        }

        [HttpPost]
        public IActionResult AdicionaDocumento([FromBody] UpdateDocumentosDto updateDocumentosDto)
        {
            var documento = _mapper.Map<Documentos>(updateDocumentosDto);
            _context.DocumentosDB.Add(documento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RetornaDocumentoPorId), new { Id = documento.Id }, updateDocumentosDto);
        }

        [HttpPut("{id}")]
        public IActionResult EditaDocumento(int id, [FromBody] UpdateDocumentosDto updateDocumentosDto)
        {
            var documento = _context.DocumentosDB.SingleOrDefault(d => d.Id == id);
            if (documento == null)
                return NotFound();
            _mapper.Map(updateDocumentosDto, documento);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaDocumento(int id)
        {
            var documento = _context.DocumentosDB.SingleOrDefault(d => d.Id == id);
            if (documento == null)
                return NotFound();
            _context.DocumentosDB.Remove(documento);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
