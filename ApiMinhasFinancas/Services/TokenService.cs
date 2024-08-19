using BibliotecaMinhasFinancas.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiMinhasFinancas.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Usuarios usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("username", usuario.UserName)
            };

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
