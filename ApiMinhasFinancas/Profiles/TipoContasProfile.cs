using ApiMinhasFinancas.Dtos.TipoContas;
using ApiMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class TipoContasProfile: Profile
    {
        public TipoContasProfile()
        {
            CreateMap<UpdateTipoContasDto, TipoContas>();            
            CreateMap<TipoContas, ReadTipoContaDto>();       
        }
    }
}
