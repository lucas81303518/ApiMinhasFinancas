using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using BibliotecaMinhasFinancas.Dtos.Usuarios;

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
