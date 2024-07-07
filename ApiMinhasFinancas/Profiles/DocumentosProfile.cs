using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;

namespace ApiMinhasFinancas.Profiles
{
    public class DocumentosProfile: Profile
    {
        public DocumentosProfile()
        {
            CreateMap<UpdateDocumentosDto, Documentos>();
            CreateMap<Documentos, ReadDocumentosDto>();                        
        }
    }
}
