﻿using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ApiMinhasFinancas.Data
{
    public class MinhasFinancasContext: IdentityDbContext<Usuarios, IdentityRole<int>, int>
    {    
        public MinhasFinancasContext(DbContextOptions<MinhasFinancasContext> options) 
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Documentos>()
                .Property(e => e.DataDocumento)
                .HasColumnType("date");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Metas>()
                .Property(e => e.DataInsercao)
                .HasColumnType("date");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Metas>()
                .Property(e => e.DataPrevisao)
                .HasColumnType("date");
        }
        public DbSet<Usuarios> UsuariosDB { get; set; }
        public DbSet<FormasPagamento> FormasPgtoDB { get; set; }
        public DbSet<Documentos> DocumentosDB { get; set; }
        public DbSet<TipoContas> TipoContasDB { get; set; }
        public DbSet<Comprovantes> ComprovantesDB { get; set; }
        public DbSet<Metas> MetasDB { get; set; }
        public DbSet<Transferencias> TransferenciasDB { get; set; }
        public DbSet<Saldo> SaldoMensalDB { get; set; }
        public DbSet<Gastos> GastosDB { get; set; }
        public DbSet<Receitas> ReceitasDB { get; set; }
        public DbSet<MovimentacaoMetas> MovimentacaoMetasDB { get; set; }
    }
}
