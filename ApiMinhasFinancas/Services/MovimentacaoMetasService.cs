using ApiMinhasFinancas.Data;
using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Documentos;
using BibliotecaMinhasFinancas.Data.Dtos.MovimentacaoMetas;
using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiMinhasFinancas.Services
{
    public class MovimentacaoMetasService
    {
        private readonly UsuarioService _usuarioService;
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;
        public MovimentacaoMetasService(MinhasFinancasContext context, UsuarioService usuarioService, IMapper mapper)
        {
            _context = context;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task InsereMovimentacao(UpdateMovimentacaoMetas updateMovimentacaoMetas)
        {
            var movimentacaoMeta = _mapper.Map<MovimentacaoMetas>(updateMovimentacaoMetas);
            await _context.MovimentacaoMetasDB.AddAsync(movimentacaoMeta);
        }

        public async Task<IEnumerable<ReadMovimentacaoMetas>> ConsultaMovimentacaoMeta(int idMeta)
        {
            var movimentacaoMeta = await _context.MovimentacaoMetasDB
                .Where(m => m.MetaId == idMeta && m.UsuarioId == _usuarioService.GetUserId())
                .Select(Valores => new ReadMovimentacaoMetas
                {
                    DataHora = Valores.DataHora,
                    Descricao = Valores.Descricao,
                    MetaId = idMeta,
                    TipoOperacao = Valores.TipoOperacao,
                    Valor = Valores.Valor
                })
                .OrderByDescending(m=> m.DataHora)
                .ToListAsync();            
            
            return movimentacaoMeta;
        }
    }
}
