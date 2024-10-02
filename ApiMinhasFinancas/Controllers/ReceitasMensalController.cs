using ApiMinhasFinancas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Authorize(Policy = "UsuarioAtivo")]
    [Route("[controller]")]
    public class ReceitasMensalController: ControllerBase
    {
        private readonly ReceitasService _receitasService;
        public ReceitasMensalController(ReceitasService receitasService)
        {
            _receitasService = receitasService;
        }
        [HttpGet]
        public async Task<double> RecuperarReceitasMensais(
        [FromQuery]
        [Required]
        int mes,
        [FromQuery]
        [Required]
        int ano)
        {
            return await _receitasService.RecuperarReceitasMensal(mes, ano);
        }
    }
}
