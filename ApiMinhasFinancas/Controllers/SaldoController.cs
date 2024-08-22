using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SaldoController: ControllerBase
    {        
        private readonly SaldoMensalService _saldoMensalService;
        public SaldoController(SaldoMensalService saldoMensalService)
        {          
            _saldoMensalService = saldoMensalService;
        }

        [HttpGet("Total")]
        public async Task<double> SaldoTotal()
        {
            return await _saldoMensalService.SaldoTotal();
        }       
    }
}
