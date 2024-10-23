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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto dto)
        {
            var resultado = await _userService.CadastrarUsuario(dto);
            if (resultado == "OK")
            {
                return Ok();
            }
            return BadRequest(resultado);
        }

        [HttpGet("EmailJaExiste")]
        public async Task<bool> EmailJaExiste
            ([FromQuery]
            [Required]
            string email)
        {
            var retorno = await _userService.EmailJaExiste(email);
            return retorno;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuario dto)
        {
            var resultado = await _userService.Login(dto);

            if ((resultado != "Usuário não autenticado!") && 
                (resultado != "Usuário não encontrado!"))
            {
                return Ok(new { Token = resultado });
            }
            
            return Forbid(JwtBearerDefaults.AuthenticationScheme);
        }

        [Authorize(Policy = "UsuarioAtivo")]
        [HttpGet]
        public async Task<IActionResult> RecuperarUsuario()
        {
            return Ok(await _userService.RecuperarUsuario());
        }

        [Authorize(Policy = "UsuarioAtivo")]
        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario(UpdateUsuarioDto updateUsuarioDto)
        {
            var retorno = await _userService.AlterarUsuario(updateUsuarioDto);
            if (retorno == "OK")
                return Ok();
            return BadRequest(retorno);
        }

        [Authorize(Policy = "UsuarioAtivo")]
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
