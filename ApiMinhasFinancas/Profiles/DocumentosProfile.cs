using ApiMinhasFinancas.Dtos.Documentos;
using ApiMinhasFinancas.Models;
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
