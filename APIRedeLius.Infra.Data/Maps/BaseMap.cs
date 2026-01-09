using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIRedeLius.Infra.Data.Maps
{
  public class BaseMap<T> : IEntityTypeConfiguration<T> where T : class
  {
    public BaseMap() { }
    public virtual void Configure(EntityTypeBuilder<T> builder) { }
  }
}