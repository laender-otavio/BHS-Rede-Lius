using APIRedeLius.Infra.Data.Configuracao;
using APIRedeLius.Infra.Data.Contextos;
using APIRedeLius.Infra.Data.IoC;
using APIRedeLius.Servicos.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIRedeLius.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      AppSettings.Configuration = builder.Configuration;
      InfraInjection.Register(builder.Services);
      ServicoInjection.Register(builder.Services);

      var jwtKey = builder.Configuration["JwtConfig:Key"] ?? throw new ArgumentNullException("Key do JWT não encontrada.");
      var jwtIssuer = builder.Configuration["JwtConfig:ValidIssuer"] ?? throw new ArgumentNullException("Issuer do JWT não encontrada.");
      var jwtAudience = builder.Configuration["JwtConfig:ValidAudience"] ?? throw new ArgumentNullException("Audience do JWT não encontrada.");

      builder.Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(jwtOptions =>
      {
        jwtOptions.SaveToken = true;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer = jwtIssuer,
          ValidAudience = jwtAudience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
          ClockSkew = TimeSpan.Zero
        };
      });

      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("Bearer",
          new AuthorizationPolicyBuilder()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .RequireAuthenticatedUser()
          .Build());
      });

      builder.Services.AddEndpointsApiExplorer();

      builder.Services.AddSwaggerGen();

      builder.Services.AddDbContext<Contexto>(options => options.UseInMemoryDatabase("RedeLiusDB"));

      builder
        .Services
        .AddControllers()
        .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

      var app = builder.Build();

      app.UseCors("policy");
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseHttpsRedirection();

      app.MapControllers();

      app.Run();
    }
  }

}