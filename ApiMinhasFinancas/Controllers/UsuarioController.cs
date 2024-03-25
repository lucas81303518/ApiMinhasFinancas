using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Data.Dtos.Usuarios;
using ApiMinhasFinancas.Dtos.Usuarios;
using ApiMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioController: ControllerBase
    {
        private readonly MinhasFinancasContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(MinhasFinancasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult RetornaUsuarios()
        {
            return Ok(_context.UsuariosDB);
        }

        [HttpGet("{id}")]
        public IActionResult RetornaUsuarioPorId(int id)
        {
            var usuario = _context.UsuariosDB.FirstOrDefault(u => u.Id == id);
            if(usuario != null)
                return Ok(usuario);
            return NotFound();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] CredenciaisLogin credenciais)
        {                      
            var usuario = _context.UsuariosDB.FirstOrDefault(x => x.Email == credenciais.Email && x.Senha == credenciais.Senha);

            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult AdicionaUsuario([FromBody] UpdateUsuarioDto usuarioDto)
        {
            Usuarios usuario = _mapper.Map<Usuarios>(usuarioDto);
            if (_context.UsuariosDB.Any(u => u.Email == usuario.Email))
                return BadRequest("E-mail já cadastrado. Escolha um e-mail diferente.");
          
            _context.UsuariosDB.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RetornaUsuarioPorId), new { Id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult EditarUsuario(int id, [FromBody] UpdateUsuarioDto usuarioDto)
        {          
            var usuario = _context.UsuariosDB.SingleOrDefault(u => u.Id == id);
            if (usuario == null)            
                return NotFound();            
            if (usuario.Email != usuarioDto.Email && _context.UsuariosDB.Any(u => u.Email == usuarioDto.Email))            
                return BadRequest("E-mail já cadastrado. Escolha um e-mail diferente.");           
            try
            {                
                _mapper.Map(usuarioDto, usuario);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {                          
                return StatusCode(500, "Ocorreu um erro ao salvar os dados. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverUsuario(int id)
        {
            var usuario = _context.UsuariosDB.FirstOrDefault(u => u.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            _context.UsuariosDB.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
