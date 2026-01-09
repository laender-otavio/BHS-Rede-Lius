using APIRedeLius.Servicos.Interfaces;
using APIRedeLius.Servicos.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace APIRedeLius.Servicos.IoC
{
  public class ServicoInjection
  {
    public static void Register(IServiceCollection services)
    {
      services.AddScoped<IAutenticacaoService, AutenticacaoService>();
      services.AddScoped<IAlunoService, AlunoService>();
    }
  }
}