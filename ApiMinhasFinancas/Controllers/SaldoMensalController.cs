using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SaldoMensalController: ControllerBase
    {
        private MinhasFinancasContext _context;
        private UsuarioService _usuarioService;
        public SaldoMensalController(MinhasFinancasContext context, UsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        [HttpGet("SaldoTotal")]
        public async Task<double> SaldoTotal()
        {
            var saldoTotal = await _context.SaldoMensalDB
                .Where(s => s.UsuarioId == _usuarioService.GetUserId())
                .SumAsync(s => s.ValorTotal);
            return saldoTotal;
        }

        [HttpGet]
        public async Task<double> SaldoTotal(
            [FromQuery(Name = "mes")]
            [Required]
            int mes,
            [FromQuery(Name = "ano")]
            [Required]
            int ano)
        {
            var saldoTotal = await _context.SaldoMensalDB
                .Where(s => s.UsuarioId == _usuarioService.GetUserId())
                .Where(s => s.Mes == mes && s.Ano == ano)
                .SumAsync(s => s.ValorTotal);
            return saldoTotal;
        }
    }
}
