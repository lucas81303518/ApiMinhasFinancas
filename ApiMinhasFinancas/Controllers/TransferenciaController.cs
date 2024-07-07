using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Transferencias;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMinhasFinancas.Dtos.Transferencias;

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
        public async Task<IEnumerable<ReadTransferenciasDto>> ObterTransferencias()
        {
            return _mapper.Map<List<ReadTransferenciasDto>>(await _context.TransferenciasDB.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTransferenciaPorId(int id)
        {
            var transferencias = await _context.TransferenciasDB.SingleOrDefaultAsync(t=> t.Id == id);
            if(transferencias != null)
            {
                ReadTransferenciasDto readTransferenciasDto = _mapper.Map<ReadTransferenciasDto>(transferencias);
                return Ok(readTransferenciasDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaTransferencia([FromBody] UpdateTransferenciasDto updateTransferenciasDto)
        {
            Transferencias transferencias = _mapper.Map<Transferencias>(updateTransferenciasDto);
            _context.TransferenciasDB.Add(transferencias);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterTransferenciaPorId), new { Id = transferencias.Id }, updateTransferenciasDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditaTransferencia(int id, [FromBody] UpdateTransferenciasDto updateTransferenciasDto)
        {
            var transferencias = await _context.TransferenciasDB.SingleOrDefaultAsync(t => t.Id == id);
            if (transferencias == null)
                return NotFound();
            _mapper.Map(updateTransferenciasDto, transferencias);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaTransferencia(int id)
        {
            var transferencias = await _context.TransferenciasDB.SingleOrDefaultAsync(t => t.Id == id);
            if (transferencias == null)
                return NotFound();
            _context.TransferenciasDB.Remove(transferencias);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
