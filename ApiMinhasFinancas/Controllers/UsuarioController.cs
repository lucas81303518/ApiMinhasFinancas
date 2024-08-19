using ApiMinhasFinancas.Data;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApiMinhasFinancas.Services;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;

namespace BibliotecaMinhasFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController: ControllerBase
    {
        private readonly UsuarioService _userService;
        public UsuarioController(UsuarioService userService)
        {      
            _userService = userService;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(UpdateUsuarioDto dto)
        {
            var resultado = await _userService.CadastrarUsuario(dto);
            if (resultado.Sucesso)
            {
                return Ok(resultado.Mensagem);
            }
            return BadRequest(new { mensagem = resultado.Mensagem, erros = resultado.Erros });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuario dto)
        {
            var resultado = await _userService.Login(dto);

            if (resultado.Sucesso)
            {
                return Ok(new { Token = resultado.Mensagem });
            }
            else
            {
                return Unauthorized(resultado.Mensagem);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RecuperarUsuario()
        {
            return Ok(await _userService.RecuperarUsuario());
        }

        [Authorize]
        [HttpPut("AtualizarFoto")]
        public async Task<IActionResult> AtualizarFoto([FromBody] UpdateFoto fotoBase64)
        {
            var resultado = await _userService.AtualizarFoto(fotoBase64);
            if(resultado == "OK")
                return NoContent();           

            return BadRequest(resultado);
        }
    }
}
