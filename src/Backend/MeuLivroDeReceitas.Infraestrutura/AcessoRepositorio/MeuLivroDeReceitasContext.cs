using MeuLivroDeReceitas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infraestrutura.AcessoRepositorio;

    public class MeuLivroDeReceitasContext : DbContext
    {
    public MeuLivroDeReceitasContext(DbContextOptions<MeuLivroDeReceitasContext> options) : base(options)
        {    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuLivroDeReceitasContext).Assembly);
    }
}

