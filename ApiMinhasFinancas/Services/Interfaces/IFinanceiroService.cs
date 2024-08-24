using BibliotecaMinhasFinancas.Data.Dtos.Gastos;

namespace ApiMinhasFinancas.Services.Interfaces
{
    public interface IFinanceiroService
    {        
        public Task AtualizarValorTotalAsync(UpdateFianceirosDto updateFinanceiroDto);
    }
}
