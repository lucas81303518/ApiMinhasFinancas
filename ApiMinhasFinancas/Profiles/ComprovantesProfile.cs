using ApiMinhasFinancas.Dtos.Comprovantes;
using ApiMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class ComprovantesProfile: Profile
    {
        public ComprovantesProfile()
        {
            CreateMap<UpdateComprovantesDto, Comprovantes>();
            CreateMap<Comprovantes, UpdateComprovantesDto>();
        }
    }
}
