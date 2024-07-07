using BibliotecaMinhasFinancas.Data.Dtos.TipoContas;
using BibliotecaMinhasFinancas.Models;
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
