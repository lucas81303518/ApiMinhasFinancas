using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Identity;

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
                return new CadastroUsuarioResultado
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar usuário!",
                    Erros = resultado.Errors.Select(e => e.Description)
                };
            }
        }

        public async Task<string> Login(LoginUsuario dto)
        {
            var resultado = await 
                _userSign.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new Exception("Usuário não autenticado!");
            }

            var usuario = _userSign
                          .UserManager
                          .Users.FirstOrDefault(u => u.NormalizedUserName == dto.UserName.ToUpper());

            return _tokenService.GenerateToken(usuario);            
        }
    }
}
