using APIRedeLius.Dominio.Interfaces;
using APIRedeLius.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace APIRedeLius.Infra.Data.IoC
{
  public class InfraInjection
  {
    public static void Register(IServiceCollection services)
    {
      services.AddScoped<IAlunoRepository, AlunoRepository>();
    }
  }
}