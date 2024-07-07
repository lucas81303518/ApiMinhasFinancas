using BibliotecaMinhasFinancas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ApiMinhasFinancas.Data
{
    public class MinhasFinancasContext: DbContext
    {    
        public MinhasFinancasContext(DbContextOptions<MinhasFinancasContext> options) : base(options)
        {
            
        }
        
        public DbSet<Usuarios> UsuariosDB { get; set; }
        public DbSet<FormasPagamento> FormasPgtoDB { get; set; }
        public DbSet<Documentos> DocumentosDB { get; set; }
        public DbSet<TipoContas> TipoContasDB { get; set; }
        public DbSet<Comprovantes> ComprovantesDB { get; set; }
        public DbSet<Metas> MetasDB { get; set; }
        public DbSet<Transferencias> TransferenciasDB { get; set; }

    }
}
