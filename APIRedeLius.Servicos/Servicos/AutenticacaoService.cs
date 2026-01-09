using APIRedeLius.Infra.Data.Configuracao;
using APIRedeLius.Servicos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace APIRedeLius.Servicos.Servicos
{
  public class AutenticacaoService : IAutenticacaoService
  {
    public string Autenticar()
    {
      var jwtKey = AppSettings.Configuration?["JwtConfig:Key"] ?? throw new ArgumentNullException("Key do JWT não encontrada.");

      var tokenOptions = new JwtSecurityToken(
        issuer: AppSettings.Configuration?["JwtConfig:ValidIssuer"],
        audience: AppSettings.Configuration?["JwtConfig:ValidAudience"],
        expires: DateTime.Now.AddMinutes(int.Parse(AppSettings.Configuration?["JwtConfig:TokenLifetime"] ?? "1")),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256));

      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
  }
}