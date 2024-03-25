using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Data.Dtos.Transferencias;
using ApiMinhasFinancas.Dtos.Transferencias;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransferenciaController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public TransferenciaController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ReadTransferenciasDto> ObterTransferencias()
        {
            return _mapper.Map<List<ReadTransferenciasDto>>(_context.TransferenciasDB.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ObterTransferenciaPorId(int id)
        {
            var transferencias = _context.TransferenciasDB.SingleOrDefault(t=> t.Id == id);
            if(transferencias != null)
            {
                ReadTransferenciasDto readTransferenciasDto = _mapper.Map<ReadTransferenciasDto>(transferencias);
                return Ok(readTransferenciasDto);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaTransferencia([FromBody] UpdateTransferenciasDto updateTransferenciasDto)
        {
            Transferencias transferencias = _mapper.Map<Transferencias>(updateTransferenciasDto);
            _context.TransferenciasDB.Add(transferencias);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterTransferenciaPorId), new { Id = transferencias.Id }, updateTransferenciasDto);
        }

        [HttpPut("{id}")]
        public IActionResult EditaTransferencia(int id, [FromBody] UpdateTransferenciasDto updateTransferenciasDto)
        {
            Transferencias transferencias = _context.TransferenciasDB.SingleOrDefault(t => t.Id == id);
            if (transferencias == null)
                return NotFound();
            _mapper.Map(updateTransferenciasDto, transferencias);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaTransferencia(int id)
        {
            Transferencias transferencias = _context.TransferenciasDB.SingleOrDefault(t => t.Id == id);
            if (transferencias == null)
                return NotFound();
            _context.TransferenciasDB.Remove(transferencias);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
