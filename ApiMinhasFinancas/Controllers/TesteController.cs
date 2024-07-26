using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class TesteController: ControllerBase
    {
        [HttpGet]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}
