using BibliotecaMinhasFinancas.Data.Dtos.Metas;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using BibliotecaMinhasFinancas.Dtos.Metas;
using BibliotecaMinhasFinancas.Data.Dtos.MovimentacaoMetas;

namespace ApiMinhasFinancas.Profiles
{
    public class MetasProfile: Profile
    {
        public MetasProfile()
        {
            CreateMap<UpdateMetasDto, Metas>();
            CreateMap<Metas, ReadMetasDto>();
            CreateMap<UpdateMovimentacaoMetas, MovimentacaoMetas>();                       
        }
    }
}
