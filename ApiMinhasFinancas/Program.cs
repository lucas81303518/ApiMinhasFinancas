using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Factorys;
using ApiMinhasFinancas.Services;
using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

/*AddDbContext (Utilizado para configurar [Authorize(Policy = "UsuarioAtivo")] Contexto do banco de dados (MinhasFinancasContext))
 * UseLazyLoadingProxies (Utilizado para fazer [Authorize(Policy = "UsuarioAtivo")] carregamento dos Models que possuem SubObjetos)
 * Exemplo: Model Documentos que possui FormaPagamento, TipoContas [Authorize(Policy = "UsuarioAtivo")] Usuario
 * AutoMapper (Pasta Profiles é destinada aos Mapeamentos) (Utilizado para Mapear de uma EntidadeModel para EntidadeDto [Authorize(Policy = "UsuarioAtivo")] Vice-Versa)
 * 
*/

//builder.Services.AddDbContext<MinhasFinancasContext>
//  (options =>
//      options.UseLazyLoadingProxies()
//             .UseInMemoryDatabase(databaseName: "TestDatabase"));

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<MinhasFinancasContext>
    (options =>
    {
        options.UseLazyLoadingProxies()
               .UseNpgsql(connectionString);
    });

builder.Services
    .AddIdentity<Usuarios, IdentityRole<int>>(options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;     
    })
    .AddEntityFrameworkStores<MinhasFinancasContext>()
    .AddDefaultTokenProviders();
// Factorys \\
builder.Services.AddScoped<FinanceiroFactory>();
// Services \\
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<SaldoMensalService>();
builder.Services.AddScoped<TipoContasService>();
builder.Services.AddScoped<FinanceiroService>(); 
builder.Services.AddScoped<GastosService>();
builder.Services.AddScoped<ReceitasService>();
builder.Services.AddScoped<MovimentacaoMetasService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.WebHost.UseUrls("http://*:5000", "https://*:5001");
var SymetricSecurityKey = builder.Configuration["SymmetricSecurityKey"];

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var tokenValidator = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
            await tokenValidator.ValidateTokenAsync(context);
        }
    };

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(SymetricSecurityKey)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsuarioAtivo", policy =>
        policy.RequireClaim("Situacao", "Ativo"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<MinhasFinancasContext>();
    dbContext.Database.Migrate();
    
    var userManager = services.GetRequiredService<UserManager<Usuarios>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await DatabaseInicializer.SeedRolesAndUsers(userManager, roleManager); 
}

app.MapControllers();

app.Run();
