using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Gastos;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;

namespace ApiMinhasFinancas.Profiles
{
    public class GastosProfile: Profile
    {
        public GastosProfile() 
        {                        
            CreateMap<UpdateFianceirosDto, Gastos>();           
        }
    }
}
