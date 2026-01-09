using APIRedeLius.Dominio.Entidades;
using APIRedeLius.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace APIRedeLius.Infra.Data.Contextos
{
  public class Contexto : DbContext
  {
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Aluno> Alunos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new AlunoMap());
    }
  }
}