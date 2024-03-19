using ApiMinhasFinancas.Dtos.Usuarios;
using ApiMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class UsuariosProfile: Profile
    {
        public UsuariosProfile()
        {
            CreateMap<UpdateUsuarioDto, Usuarios>();     
            CreateMap<Usuarios, ReadUsuariosDto>();
        }
    }
}
