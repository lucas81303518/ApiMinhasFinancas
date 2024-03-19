using ApiMinhasFinancas.Data.Dtos.Metas;
using ApiMinhasFinancas.Dtos.Metas;
using ApiMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class MetasProfile: Profile
    {
        public MetasProfile()
        {
            CreateMap<UpdateMetasDto, Metas>();
            CreateMap<Metas, ReadMetasDto>();
        }
    }
}
