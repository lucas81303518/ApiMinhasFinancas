using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ApiMinhasFinancas.Services;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UsuarioAtivo")]
    public class DocumentoController: ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;        
        private readonly SaldoMensalService _saldoMensalService;
        private readonly TipoContasService _tipoContasService;        
        private FinanceiroService _financeiroService;

        public DocumentoController(MinhasFinancasContext context, 
                                   IMapper mapper, UsuarioService usuarioService,
                                   SaldoMensalService saldoMensalService, 
                                   TipoContasService tipoContasService, 
                                   FinanceiroService financeiroService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _saldoMensalService = saldoMensalService;
            _tipoContasService = tipoContasService;        
            _financeiroService = financeiroService;
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

        [HttpGet("Extrato")]
        public async Task<IEnumerable<ReadDocumentosDto>> ObterDocumentosPorPeriodo(                 
            [FromQuery(Name = "dataIni")]
            [Required] DateTime dataIni,
            [FromQuery(Name = "dataFim")]
            [Required] DateTime dataFim)
        {
            var documentos = await _context.DocumentosDB
                                     .Where(d => d.UsuarioId == _usuarioService.GetUserId())
                                     .Where(d => d.DataDocumento >= dataIni.Date &&
                                            d.DataDocumento <= dataFim.Date)
                                     .OrderByDescending(d => d.DataDocumento)
                                     .ToListAsync();
            return _mapper.Map<List<ReadDocumentosDto>>(documentos);
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
                                     .Where(d => d.DataDocumento >= dataIni.Date &&
                                            d.DataDocumento <= dataFim.Date && 
                                            d.TipoConta.Tipo == tipo && d.Status == status.ToString())
                                     .OrderBy(d => d.DataDocumento)
                                     .ToListAsync();
            return _mapper.Map<List<ReadDocumentosDto>>(documentos);            
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

        [HttpPost]
        public async Task<IActionResult> AdicionaDocumento([FromBody] UpdateDocumentosDto updateDocumentosDto)
        {
            updateDocumentosDto.UsuarioId = _usuarioService.GetUserId();
            var documento = _mapper.Map<Documentos>(updateDocumentosDto);
            _context.DocumentosDB.Add(documento);

            await _saldoMensalService.CriarOuAtualizarSaldoAsync
                (new SaldoDto
                {
                    TipoOperacao   = TipoOperacao.Inserir,
                    TipoDocumento  = await _tipoContasService.GetTipo(updateDocumentosDto.TipoContaId),
                    ValorDocumento = updateDocumentosDto.Valor
                });
            
            await _financeiroService.AtualizarGastoAsync
                (new UpdateFianceirosDto
                {
                    Ano = updateDocumentosDto.DataDocumento.Year,
                    Mes = updateDocumentosDto.DataDocumento.Month,
                    ValorDocumento = updateDocumentosDto.Valor,                   
                    TipoOperacao = TipoOperacao.Inserir,                    
                    tipoDocumento = await _tipoContasService.GetTipo(documento.TipoContaId)
                });
            
            await _context.SaveChangesAsync();
            return Created(nameof(RetornaDocumentoPorId), new { id = documento.Id});            
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

            await _saldoMensalService.CriarOuAtualizarSaldoAsync
                (new SaldoDto
                {
                    TipoOperacao = TipoOperacao.Alterar,
                    TipoDocumento = await _tipoContasService.GetTipo(updateDocumentosDto.TipoContaId),
                    ValorDocumento = updateDocumentosDto.Valor,
                    ValorDocumentoAntigo = documento.Valor
                });

            await _financeiroService.AtualizarGastoAsync
                (new UpdateFianceirosDto
                {
                    Ano = updateDocumentosDto.DataDocumento.Year,
                    Mes = updateDocumentosDto.DataDocumento.Month,
                    ValorDocumento = updateDocumentosDto.Valor,
                    AnoAntigo = documento.DataDocumento.Year,
                    MesAntigo = documento.DataDocumento.Month,
                    ValorDocumentoAntigo = documento.Valor,
                    TipoOperacao = TipoOperacao.Alterar,                    
                    tipoDocumento = await _tipoContasService.GetTipo(documento.TipoContaId)
                });

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

            await _saldoMensalService.CriarOuAtualizarSaldoAsync
                (new SaldoDto
                {
                    TipoOperacao = TipoOperacao.Deletar,
                    TipoDocumento = await _tipoContasService.GetTipo(documento.TipoContaId),
                    ValorDocumento = documento.Valor                   
                });

            await _financeiroService.AtualizarGastoAsync
                (new UpdateFianceirosDto
                {                   
                    Ano = documento.DataDocumento.Year,
                    Mes = documento.DataDocumento.Month,
                    ValorDocumento = documento.Valor,
                    TipoOperacao = TipoOperacao.Deletar,
                    tipoDocumento = await _tipoContasService.GetTipo(documento.TipoContaId)
                });

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
