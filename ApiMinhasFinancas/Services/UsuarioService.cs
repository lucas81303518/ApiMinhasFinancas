using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using BibliotecaMinhasFinancas.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiMinhasFinancas.Services
{
    public interface IUserService
    {
        int GetUserId();
        string GetUsername();
    }

    public class CadastroUsuarioResultado
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<string> Erros { get; set; }
    }

    public class UsuarioService : IUserService
    {
        private IMapper _mapper;
        private UserManager<Usuarios> _userManager;
        private SignInManager<Usuarios> _userSign;
        private TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(IMapper mapper, UserManager<Usuarios> userManager, SignInManager<Usuarios> userSign, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userSign = userSign;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst("id")?.Value);
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;
        }
        public async Task<ReadUsuariosDto> RecuperarUsuario()
        {           
            int userId = GetUserId();        
            var usuario = await _userManager.FindByIdAsync(userId.ToString());
            if (usuario == null)
            {
                return null; 
            }           
            var usuarioDto = _mapper.Map<ReadUsuariosDto>(usuario);

            return usuarioDto;
        }

        public async Task<string> AtualizarFoto(UpdateFoto fotoBase64)
        {
            if (!Funcoes.IsBase64String(fotoBase64.FotoBase64))
                return "Base 64 inválido!";
            
            var usuario = await _userManager.FindByIdAsync(GetUserId().ToString());
            if (usuario == null)
                return "Usuário não encontrado!";
          
            usuario.FotoBase64 = fotoBase64.FotoBase64;
            IdentityResult resultado = await _userManager.UpdateAsync(usuario);
            if (! resultado.Succeeded)
            {
                return "Erro ao atualizar foto do usuário!";                
            }

            return "OK";
        }

        public async Task<CadastroUsuarioResultado> CadastrarUsuario(UpdateUsuarioDto dto)
        {
            Usuarios usuario = _mapper.Map<Usuarios>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (resultado.Succeeded)
            {
                return new CadastroUsuarioResultado
                {
                    Sucesso = true,
                    Mensagem = "Usuário cadastrado com sucesso!"
                };
            }
            else
            {
                var erros = resultado.Errors.Select(e => e.Description).ToList();
                var mensagem = "Por favor, corrija os seguintes erros: " + string.Join(", ", erros);

                return new CadastroUsuarioResultado
                {
                    Sucesso = false,
                    Mensagem = mensagem,
                    Erros = erros
                };
            }
        }
        public async Task<CadastroUsuarioResultado> Login(LoginUsuario dto)
        {
            var resultado = await _userSign.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                return new CadastroUsuarioResultado
                {
                    Sucesso = false,
                    Mensagem = "Usuário não autenticado!"
                };
            }

            var usuario = _userSign
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedUserName == dto.UserName.ToUpper());

            if (usuario == null)
            {
                return new CadastroUsuarioResultado
                {
                    Sucesso = false,
                    Mensagem = "Usuário não encontrado!"
                };
            }

            var token = _tokenService.GenerateToken(usuario);

            return new CadastroUsuarioResultado
            {
                Sucesso = true,
                Mensagem = token
            };
        }
    }
}
