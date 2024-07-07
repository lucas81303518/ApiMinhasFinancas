using BibliotecaMinhasFinancas.Data.Dtos.FormasPagamento;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class FormasPagamentoProfile: Profile    
    {
        public FormasPagamentoProfile()
        {
            CreateMap<UpdateFormasPagamentoDto, FormasPagamento>();           
            CreateMap<FormasPagamento, ReadFormaPagamentoDto>();         
        }
    }
}
