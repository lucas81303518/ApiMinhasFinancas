using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services.Interfaces;
using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;
using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Services
{
    public class GastosService: IFinanceiroService
    {
        private readonly MinhasFinancasContext _context;
        private readonly UsuarioService _usuarioService;           
        public GastosService(MinhasFinancasContext context, 
                             UsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;        
        }

        public async Task<double> RecuperarGastoMensal(int mes, int ano)
        {
            var gastos = await _context.GastosDB
                .FirstOrDefaultAsync(g => g.Mes == mes && g.Ano == ano &&
                g.UsuarioId == _usuarioService.GetUserId());
            if (gastos == null)
                return 0;
            return gastos.ValorTotal;
        }
       
        public async Task AtualizarValorTotalAsync(UpdateFianceirosDto updateFinanceiroDto)
        {
            var gastos = await _context.GastosDB
                .FirstOrDefaultAsync(g => g.UsuarioId == _usuarioService.GetUserId() &&
                                          g.Ano == updateFinanceiroDto.Ano &&
                                          g.Mes == updateFinanceiroDto.Mes);

            if (gastos == null)
            {               
                if (updateFinanceiroDto.TipoOperacao != TipoOperacao.Deletar)
                {
                    gastos = new Gastos
                    {
                        Ano = updateFinanceiroDto.Ano,
                        Mes = updateFinanceiroDto.Mes,
                        UsuarioId = _usuarioService.GetUserId(),
                        ValorTotal = updateFinanceiroDto.TipoOperacao == TipoOperacao.Inserir
                                     ? updateFinanceiroDto.ValorDocumento
                                     : 0
                    };
                    _context.GastosDB.Add(gastos);
                }
            }
            else
            {
                switch (updateFinanceiroDto.TipoOperacao)
                {
                    case TipoOperacao.Inserir:
                        gastos.ValorTotal += updateFinanceiroDto.ValorDocumento;
                        break;

                    case TipoOperacao.Alterar:
                        gastos.ValorTotal = gastos.ValorTotal - updateFinanceiroDto.ValorDocumentoAntigo + updateFinanceiroDto.ValorDocumento;
                        break;

                    case TipoOperacao.Deletar:
                        gastos.ValorTotal -= updateFinanceiroDto.ValorDocumento;
                        break;
                }

                if (gastos.Id != 0)
                {
                    _context.GastosDB.Update(gastos);
                }
            }
        }

    }
}
