using ApiMinhasFinancas.Data.Dtos.Transferencias;
using ApiMinhasFinancas.Dtos.Transferencias;
using ApiMinhasFinancas.Models;
using AutoMapper;

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
