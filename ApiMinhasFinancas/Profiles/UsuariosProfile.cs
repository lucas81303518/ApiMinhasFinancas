
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;

namespace ApiMinhasFinancas.Profiles
{
    public class UsuariosProfile: Profile
    {
        public UsuariosProfile()
        {
            CreateMap<CreateUsuarioDto, Usuarios>();     
            CreateMap<Usuarios, ReadUsuariosDto>();
            CreateMap<UpdateUsuarioDto, Usuarios>();
        }
    }
}
