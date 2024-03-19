using ApiMinhasFinancas.Dtos.FormasPagamento;
using ApiMinhasFinancas.Models;
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
