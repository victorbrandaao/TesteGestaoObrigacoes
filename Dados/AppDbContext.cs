using GestaoObrigacoes.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestaoObrigacoes.Dados
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<ObrigacaoAcessoria> ObrigacoesAcessorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamento entre Empresa e ObrigacaoAcessoria
            modelBuilder.Entity<ObrigacaoAcessoria>()
                .HasOne(o => o.Empresa)
                .WithMany(e => e.Obrigacoes)
                .HasForeignKey(o => o.EmpresaId);
        }
    }
}
