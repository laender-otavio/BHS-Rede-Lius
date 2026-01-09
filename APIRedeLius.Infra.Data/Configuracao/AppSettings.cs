using Microsoft.Extensions.Configuration;

namespace APIRedeLius.Infra.Data.Configuracao
{
  public class AppSettings
  {
    public static IConfiguration? Configuration { get; set; } = null;
  }
}