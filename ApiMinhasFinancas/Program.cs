using ApiMinhasFinancas.Data;
using ApiMinhasFinancas.Services;
using BibliotecaMinhasFinancas.Data.Dtos.Usuarios;
using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

/*AddDbContext (Utilizado para configurar o Contexto do banco de dados (MinhasFinancasContext))
 * UseLazyLoadingProxies (Utilizado para fazer o carregamento dos Models que possuem SubObjetos)
 * Exemplo: Model Documentos que possui FormaPagamento, TipoContas e Usuario
 * AutoMapper (Pasta Profiles é destinada aos Mapeamentos) (Utilizado para Mapear de uma EntidadeModel para EntidadeDto e Vice-Versa)
 * 
*/
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<MinhasFinancasContext>(options => options.UseLazyLoadingProxies().UseInMemoryDatabase(databaseName: "TestDatabase"));

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

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.WebHost.UseUrls("http://*:5000");
var SymetricSecurityKey = builder.Configuration["SymmetricSecurityKey"];
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
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
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<MinhasFinancasContext>();   
    dbContext.Database.Migrate();
}
app.MapControllers();

app.Run();
