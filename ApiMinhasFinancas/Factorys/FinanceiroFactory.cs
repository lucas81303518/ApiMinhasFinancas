using ApiMinhasFinancas.Services;
using ApiMinhasFinancas.Services.Interfaces;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;

namespace ApiMinhasFinancas.Factorys
{
    public class FinanceiroFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FinanceiroFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFinanceiroService CreateService(TipoDocumento tipoDocumento)
        {
            return tipoDocumento switch
            {
                TipoDocumento.Saida   => _serviceProvider.GetRequiredService<GastosService>(),
                TipoDocumento.Entrada => _serviceProvider.GetRequiredService<ReceitasService>(),
                _ => throw new ArgumentException("Invalid type", nameof(tipoDocumento))
            };
        }
    }
}
