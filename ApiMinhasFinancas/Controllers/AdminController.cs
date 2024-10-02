using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiMinhasFinancas.Data;
using Microsoft.Extensions.Caching.Memory;
using BibliotecaMinhasFinancas.Models;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrador")]
public class AdminController : ControllerBase
{
    private readonly MinhasFinancasContext _context;
    private readonly IMemoryCache _cache;

    public AdminController(MinhasFinancasContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpPut("alterarSituacao/{usuarioId}")]
    public async Task<IActionResult> AlterarSituacao(int usuarioId, [FromBody] bool novaSituacao)
    {
        var usuario = await _context.UsuariosDB.FindAsync(usuarioId);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        usuario.Situacao = novaSituacao;
        await _context.SaveChangesAsync();

        _cache.Remove(usuarioId.ToString());
        
        var updatedUserInfo = new Usuarios
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Situacao = usuario.Situacao
        };
        
        _cache.Set(usuarioId.ToString(), updatedUserInfo, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });

        return Ok($"Situação do usuário {usuario.NomeCompleto} alterada para {novaSituacao}.");
    }
}
