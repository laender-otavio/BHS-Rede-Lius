using APIRedeLius.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIRedeLius.Infra.Data.Maps
{
  public class AlunoMap : BaseMap<Aluno>
  {
    public override void Configure(EntityTypeBuilder<Aluno> builder)
    {
      base.Configure(builder);

      builder.ToTable("alunos");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
      builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(100)").HasMaxLength(100);
      builder.Property(x => x.Email).IsRequired().HasColumnName("Email").HasColumnType("varchar(100)").HasMaxLength(100);
      builder.Property(x => x.Serie).IsRequired().HasColumnName("Serie").HasColumnType("varchar(100)").HasMaxLength(100);
    }
  }
}