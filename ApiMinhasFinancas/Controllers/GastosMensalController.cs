using ApiMinhasFinancas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GastosMensalController
    {
        private readonly GastosService _gastosService;
        public GastosMensalController(GastosService gastosService)
        {
            _gastosService = gastosService;
        }

        [HttpGet]
        public async Task<double> RecuperarGastoMensal(
            [FromQuery]
            [Required]
             int mes,
            [FromQuery]
             [Required]
             int ano)
        {
            return await _gastosService.RecuperarGastoMensal(mes, ano);
        }
    }
}
