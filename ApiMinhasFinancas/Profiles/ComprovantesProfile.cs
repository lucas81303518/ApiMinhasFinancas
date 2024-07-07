using BibliotecaMinhasFinancas.Data.Dtos.Comprovantes;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;

namespace BibliotecaMinhasFinancas.Profiles
{
    public class ComprovantesProfile: Profile
    {
        public ComprovantesProfile()
        {
            CreateMap<UpdateComprovantesDto, Comprovantes>();
            CreateMap<Comprovantes, ReadComprovanteDto>();
        }
    }
}
