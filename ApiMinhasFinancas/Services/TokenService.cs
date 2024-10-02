using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApiMinhasFinancas.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;
        private UserManager<Usuarios> _userManager;
        private readonly IMemoryCache _cache;

        public TokenService(IConfiguration configuration, UserManager<Usuarios> userManager, IMemoryCache cache)
        {
            _configuration = configuration;
            _userManager = userManager;
            _cache = cache;
        }
        public async Task ValidateTokenAsync(TokenValidatedContext context)
        {
            var userId = context.Principal.FindFirst("id")?.Value;

            if (!_cache.TryGetValue(userId, out Usuarios cachedUser))
            {
                cachedUser = await _userManager.FindByIdAsync(userId);
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };
                _cache.Set(userId, cachedUser, cacheEntryOptions);
            }

            if (cachedUser == null || !cachedUser.Situacao)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Usuário está inativo");
                context.Fail("Usuário está inativo");
            }
        }

        public async Task<string> GenerateToken(Usuarios usuario)
        {
            var usuarioRoles = await _userManager.GetRolesAsync(usuario);
            var claims = new List<Claim>
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("username", usuario.UserName),
                new Claim("Situacao", usuario.Situacao ? "Ativo" : "Inativo")
            };
            
            if (usuarioRoles.Contains("Administrador"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }

            var chave = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

            var sigingCredentials = new SigningCredentials(chave,
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddDays(30),
                claims: claims,
                signingCredentials: sigingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
