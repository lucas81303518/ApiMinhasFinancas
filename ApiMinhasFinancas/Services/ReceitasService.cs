using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services.Interfaces;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;
using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Services
{
    public class ReceitasService: IFinanceiroService
    {
        private readonly MinhasFinancasContext _context;
        private readonly UsuarioService _usuarioService;
        public ReceitasService(MinhasFinancasContext context, UsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }
        public async Task<double> RecuperarReceitasMensal(int mes, int ano)
        {
            var receitas = await _context.ReceitasDB
                .FirstOrDefaultAsync(g => g.Mes == mes && g.Ano == ano &&
                g.UsuarioId == _usuarioService.GetUserId());
            if (receitas == null)
                return 0;
            return receitas.ValorTotal;
        }

        public async Task AtualizarValorTotalAsync(UpdateFianceirosDto updateFinanceiroDto)
        {
            var receitas = await _context.ReceitasDB
                .FirstOrDefaultAsync(g => g.UsuarioId == _usuarioService.GetUserId() &&
                                          g.Ano == updateFinanceiroDto.Ano &&
                                          g.Mes == updateFinanceiroDto.Mes);

            if (receitas == null)
            {
                if (updateFinanceiroDto.TipoOperacao != TipoOperacao.Deletar)
                {
                    receitas = new Receitas
                    {
                        Ano = updateFinanceiroDto.Ano,
                        Mes = updateFinanceiroDto.Mes,
                        UsuarioId = _usuarioService.GetUserId(),
                        ValorTotal = updateFinanceiroDto.TipoOperacao == TipoOperacao.Inserir
                                     ? updateFinanceiroDto.ValorDocumento
                                     : 0
                    };
                    _context.ReceitasDB.Add(receitas);
                }
            }
            else
            {
                switch (updateFinanceiroDto.TipoOperacao)
                {
                    case TipoOperacao.Inserir:
                        receitas.ValorTotal += updateFinanceiroDto.ValorDocumento;
                        break;

                    case TipoOperacao.Alterar:
                        receitas.ValorTotal = receitas.ValorTotal - updateFinanceiroDto.ValorDocumentoAntigo + updateFinanceiroDto.ValorDocumento;
                        break;

                    case TipoOperacao.Deletar:
                        receitas.ValorTotal -= updateFinanceiroDto.ValorDocumento;
                        break;
                }

                if (receitas.Id != 0)
                {
                    _context.ReceitasDB.Update(receitas);
                }
            }
        }
    }
}
