using AutoMapper;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using BibliotecaMinhasFinancas.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMinhasFinancas.Services
{
    public interface IUserService
    {
        int GetUserId();
        string GetUsername();
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
        
        public async Task<bool> EmailJaExiste(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            return usuario != null;
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

        public async Task<string> CadastrarUsuario(CreateUsuarioDto dto)
        {
            Usuarios usuario = _mapper.Map<Usuarios>(dto);
            try
            {
                IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
                if (resultado.Succeeded)
                {
                    return "OK";
                }
              
                if (resultado.Errors.ToArray()[0].Code.Contains("DuplicateUserName"))
                {
                    return "Usuário informado já está sendo utilizado!";
                }
                return "Erro ao inserir usuário";                
            }
            catch (DbUpdateException ex) 
            {
                if (ex.InnerException.Message.Contains("duplicar valor da chave viola a restrição de unicidade \"IX_AspNetUsers_Email"))
                {
                    return "E-mail inserido já está sendo utilizado!";
                }          
                
                return "Erro ao inserir usuário " + ex.Message;
            }                       
        }

        public async Task<string> AlterarUsuario(UpdateUsuarioDto updateUsuarioDto)
        {
            int userId = GetUserId();
            var usuario = await _userManager.FindByIdAsync(userId.ToString());
            if (usuario == null)
            {
                return "Usuário não encontrado!";
            }
            _mapper.Map(updateUsuarioDto, usuario);
            IdentityResult resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded)
                return "OK";
            return "Erro ao Atualizar usuário!";
        }

        public async Task<string> Login(LoginUsuario dto)
        {
            var resultado = await _userSign.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                return "Usuário não autenticado!";
            }

            var usuario = _userSign
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedUserName == dto.UserName.ToUpper());

            if (usuario == null)
            {
                return "Usuário não encontrado!";               
            }

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
