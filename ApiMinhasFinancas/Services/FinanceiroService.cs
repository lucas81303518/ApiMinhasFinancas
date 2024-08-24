using ApiMinhasFinancas.Factorys;
using ApiMinhasFinancas.Services.Interfaces;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;

namespace ApiMinhasFinancas.Services
{
    public class FinanceiroService
    {            
        private readonly FinanceiroFactory _financeiroFactory;
        public FinanceiroService(FinanceiroFactory financeiroFactory)
        {           
            _financeiroFactory = financeiroFactory;            
        }

        public async Task AtualizarGastoAsync(UpdateFianceirosDto updateFinanceiroDto)
        {
            IFinanceiroService _IfinanceiroService = _financeiroFactory
                .CreateService(updateFinanceiroDto.tipoDocumento);
            if (updateFinanceiroDto.TipoOperacao == TipoOperacao.Alterar &&
                (updateFinanceiroDto.AnoAntigo != updateFinanceiroDto.Ano || updateFinanceiroDto.MesAntigo != updateFinanceiroDto.Mes))
            {
                await _IfinanceiroService.AtualizarValorTotalAsync(new UpdateFianceirosDto
                {
                    Ano = updateFinanceiroDto.AnoAntigo,
                    Mes = updateFinanceiroDto.MesAntigo,
                    ValorDocumento = updateFinanceiroDto.ValorDocumentoAntigo,
                    TipoOperacao = TipoOperacao.Deletar
                });

                await _IfinanceiroService.AtualizarValorTotalAsync(new UpdateFianceirosDto
                {
                    Ano = updateFinanceiroDto.Ano,
                    Mes = updateFinanceiroDto.Mes,
                    ValorDocumento = updateFinanceiroDto.ValorDocumento,
                    TipoOperacao = TipoOperacao.Inserir
                });
            }
            else
            {
                await _IfinanceiroService.AtualizarValorTotalAsync(updateFinanceiroDto);
            }
        }
    }
}
