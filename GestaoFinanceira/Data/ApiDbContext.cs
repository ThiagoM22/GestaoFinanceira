using GestaoFinanceira.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestaoFinanceira.Data
{
    public class ApiDbContext : IdentityDbContext<Usuario> // Alterado para herdar de IdentityDbContext<Usuario>
    {
        // Construtor que recebe as opções de configuração do DbContext
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        // Definição dos DbSets para as entidades do modelo
        public DbSet<Lucro> Lucros { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<CategoriaLucro> CategoriaLucros { get; set; }
        public DbSet<CategoriaGasto> CategoriaGastos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar comportamento de deleção para evitar ciclos de cascade
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.CategoriaGasto)
                .WithMany()
                .HasForeignKey(g => g.CategoriaGastoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lucro>()
                .HasOne(l => l.CategoriaLucro)
                .WithMany()
                .HasForeignKey(l => l.CategoriaLucroId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
