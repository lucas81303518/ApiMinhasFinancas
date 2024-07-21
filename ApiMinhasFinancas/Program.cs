using ApiMinhasFinancas.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

/*AddDbContext (Utilizado para configurar o Contexto do banco de dados (MinhasFinancasContext))
 * UseLazyLoadingProxies (Utilizado para fazer o carregamento dos Models que possuem SubObjetos)
 * Exemplo: Model Documentos que possui FormaPagamento, TipoContas e Usuario
 * AutoMapper (Pasta Profiles é destinada aos Mapeamentos) (Utilizado para Mapear de uma EntidadeModel para EntidadeDto e Vice-Versa)
 * 
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
//builder.Services.AddDbContext<MinhasFinancasContext>(options => options.UseLazyLoadingProxies().UseInMemoryDatabase(databaseName: "TestDatabase"));
builder.Services.AddDbContext<MinhasFinancasContext>(options => 
options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<MinhasFinancasContext>();   
    dbContext.Database.Migrate();
}
app.MapControllers();

app.Run();
