using ApiMinhasFinancas.Data;
using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;
using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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

        public async Task<Saldo> ObterSaldoPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.SaldoMensalDB.
                FirstOrDefaultAsync(s => s.UsuarioId == usuarioId);
        }

        public async Task CriarSaldoAsync(Saldo saldo)
        {
            await _context.SaldoMensalDB.AddAsync(saldo);
        }
        
        public async Task AlterarSaldoAsync(double valorNovo)
        {
            var saldo = await ObterSaldoPorUsuarioIdAsync(_usuarioService.GetUserId());

            if (saldo == null)
            {
                throw new Exception("Saldo não encontrado para o usuário.");
            }
            saldo.ValorTotal = valorNovo;
            _context.SaldoMensalDB.Update(saldo);
        }
    
        public double AtualizarSaldo(double saldoAtual, SaldoDto saldoDto)
        {
            double novoSaldo = saldoAtual;

            switch (saldoDto.TipoOperacao)
            {
                case TipoOperacao.Inserir:
                    novoSaldo = saldoDto.TipoDocumento == TipoDocumento.Entrada
                        ? saldoAtual + saldoDto.ValorDocumento
                        : saldoAtual - saldoDto.ValorDocumento;
                    break;

                case TipoOperacao.Alterar:
                    novoSaldo = saldoDto.TipoDocumento == TipoDocumento.Entrada
                        ? saldoAtual - saldoDto.ValorDocumentoAntigo + saldoDto.ValorDocumento
                        : saldoAtual + saldoDto.ValorDocumentoAntigo - saldoDto.ValorDocumento;
                    break;

                case TipoOperacao.Deletar:
                    novoSaldo = saldoDto.TipoDocumento == TipoDocumento.Entrada
                        ? saldoAtual - saldoDto.ValorDocumento
                        : saldoAtual + saldoDto.ValorDocumento;
                    break;
            }

            return novoSaldo;
        }
        public async Task<double> SaldoTotal()
        {
            var saldoTotal = await _context.SaldoMensalDB
                .Where(s => s.UsuarioId == _usuarioService.GetUserId())
                .SumAsync(s => s.ValorTotal);
            return saldoTotal;
        }

        public async Task CriarOuAtualizarSaldoAsync(SaldoDto saldoDto)
        {
            var saldoExistente = await ObterSaldoPorUsuarioIdAsync(_usuarioService.GetUserId());

            if (saldoExistente == null)
            {                
                Saldo novoSaldo = new Saldo
                {                    
                    UsuarioId = _usuarioService.GetUserId(),
                    ValorTotal = AtualizarSaldo(0.0, saldoDto)
                };

                await CriarSaldoAsync(novoSaldo);
            }
            else
            {               
                saldoExistente.ValorTotal = AtualizarSaldo(saldoExistente.ValorTotal, saldoDto);

                await AlterarSaldoAsync(saldoExistente.ValorTotal);
            }
        }
    }
}
