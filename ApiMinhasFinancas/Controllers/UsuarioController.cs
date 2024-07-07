using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMinhasFinancas.Controllers
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
        public async Task<IActionResult> RetornaUsuarios()
        {
            return Ok(await _context.UsuariosDB.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaUsuarioPorId(int id)
        {
            var usuario = await _context.UsuariosDB.FirstOrDefaultAsync(u => u.Id == id);
            if(usuario != null)
                return Ok(usuario);
            return NotFound();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] CredenciaisLogin credenciais)
        {                      
            var usuario = await _context.UsuariosDB.FirstOrDefaultAsync(x => x.Email == credenciais.Email && x.Senha == credenciais.Senha);

            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaUsuario([FromBody] UpdateUsuarioDto usuarioDto)
        {
            Usuarios usuario = _mapper.Map<Usuarios>(usuarioDto);
            if (await _context.UsuariosDB.AnyAsync(u => u.Email == usuario.Email))
                return BadRequest("E-mail já cadastrado. Escolha um e-mail diferente.");
          
            _context.UsuariosDB.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RetornaUsuarioPorId), new { Id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] UpdateUsuarioDto usuarioDto)
        {          
            var usuario = await _context.UsuariosDB.SingleOrDefaultAsync(u => u.Id == id);
            if (usuario == null)            
                return NotFound();            
            if (usuario.Email != usuarioDto.Email && _context.UsuariosDB.Any(u => u.Email == usuarioDto.Email))            
                return BadRequest("E-mail já cadastrado. Escolha um e-mail diferente.");                                     
            _mapper.Map(usuarioDto, usuario);
            await _context.SaveChangesAsync();
            return NoContent();                       
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverUsuario(int id)
        {
            var usuario = await _context.UsuariosDB.FirstOrDefaultAsync(u => u.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            _context.UsuariosDB.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
