using ApiMinhasFinancas.Data;
using Microsoft.EntityFrameworkCore;

/*AddDbContext (Utilizado para configurar o Contexto do banco de dados (MinhasFinancasContext))
 * UseLazyLoadingProxies (Utilizado para fazer o carregamento dos Models que possuem SubObjetos)
 * Exemplo: Model Documentos que possui FormaPagamento, TipoContas e Usuario
 * AutoMapper (Utilizado para Mapear de uma EntidadeModel para EntidadeDto e Vice-Versa)
 * 
*/
var builder = WebApplication.CreateBuilder(args);            
//Descomentar para utilizar banco de dados em memória!!
//builder.Services.AddDbContext<MinhasFinancasContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"));
builder.Services.AddDbContext<MinhasFinancasContext>(options => 
options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
