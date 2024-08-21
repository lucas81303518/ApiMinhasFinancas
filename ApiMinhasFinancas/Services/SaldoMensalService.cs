using ApiMinhasFinancas.Data;
using AutoMapper;
using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Services
{
    public class SaldoMensalService
    {
        private readonly UsuarioService _usuarioService;
        private readonly MinhasFinancasContext _context;
        public SaldoMensalService(UsuarioService usuarioService, MinhasFinancasContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        public async Task AtualizarSaldoMensal(int mes, int ano, double valorDocumento)
        {
            if (valorDocumento == 0)
                return;

            var saldoMensal = await _context.SaldoMensalDB
            .FirstOrDefaultAsync(s => s.Mes == mes && s.Ano == ano && 
            s.UsuarioId == _usuarioService.GetUserId());

            if (saldoMensal == null)
            {
                saldoMensal = new SaldoMensal { Mes = mes, Ano = ano, 
                                                UsuarioId = _usuarioService.GetUserId(), 
                                                ValorTotal = valorDocumento
                                               };
                await _context.SaldoMensalDB.AddAsync(saldoMensal);
            }
            else
            {
                saldoMensal.ValorTotal += valorDocumento;
            }
        }
    }
}
