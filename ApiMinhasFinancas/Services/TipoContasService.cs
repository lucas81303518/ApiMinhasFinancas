using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Saldo;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Services
{
    public class TipoContasService
    {
        private readonly UsuarioService _usuarioService;
        private readonly MinhasFinancasContext _context;
        public TipoContasService(UsuarioService usuarioService, MinhasFinancasContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        public async Task<TipoDocumento> GetTipo(int IdTipoConta)
        {
            var tipoConta = await _context.TipoContasDB.FirstOrDefaultAsync
                (t=> t.Id == IdTipoConta && t.UsuarioId == _usuarioService.GetUserId());
            if (tipoConta == null)
                throw new Exception("tipo não encontrado!");
            return (TipoDocumento)tipoConta.Tipo;
        }
    }
}
