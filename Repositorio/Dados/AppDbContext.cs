using Microsoft.EntityFrameworkCore;
using Model.Entidades;

namespace Repositorio.Dados
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pessoa>(entidade =>
            {
                entidade.ToTable("PESSOAS");
            });

            modelBuilder.Entity<Usuario>(entidade =>
            {
                entidade.ToTable("USUARIOS");
                entidade.HasOne(u => u.Pessoa)
                        .WithOne(p => p.Usuario)
                        .HasForeignKey<Usuario>(u => u.PessoaId);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
