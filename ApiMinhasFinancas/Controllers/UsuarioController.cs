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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUsuario dto)
        {
            string token = await _userService.Login(dto);            
            return Ok(token);                       
        }

    }
}
