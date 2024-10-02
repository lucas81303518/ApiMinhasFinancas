using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UsuarioAtivo")]

    public class MovimentacaoMetasController: ControllerBase
    {
        private readonly MovimentacaoMetasService _movimentacaoMetasService;
        public MovimentacaoMetasController(MovimentacaoMetasService movimentacaoMetasService)
        {
            _movimentacaoMetasService = movimentacaoMetasService;
        }

        [HttpGet("{idMeta}")]
        public async Task<IActionResult> RecuperarMovimentacaoMeta(int idMeta)
        {
            var movimentacaoMeta = await _movimentacaoMetasService
                .ConsultaMovimentacaoMeta(idMeta);
            return Ok(movimentacaoMeta);
        }
    }
}
