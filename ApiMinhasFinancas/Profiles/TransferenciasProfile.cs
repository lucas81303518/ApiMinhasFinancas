using BibliotecaMinhasFinancas.Data.Dtos.Transferencias;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using BibliotecaMinhasFinancas.Dtos.Transferencias;

namespace ApiMinhasFinancas.Profiles
{
    public class TransferenciasProfile: Profile
    {
        public TransferenciasProfile()
        {
            CreateMap<UpdateTransferenciasDto, Transferencias>();
            CreateMap<Transferencias, ReadTransferenciasDto>();
        }
    }
}
