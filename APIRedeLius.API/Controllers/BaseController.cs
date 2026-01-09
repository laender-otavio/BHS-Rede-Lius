using APIRedeLius.Dominio.DTOs;
using APIRedeLius.Dominio.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIRedeLius.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Produces("application/json")]
  public class BaseController : ControllerBase
  {
    protected HttpStatusCode GetStatusCodeByException(Exception ex)
    {
      return ex switch
      {
        NotFoundException => HttpStatusCode.NotFound,
        ArgumentNullException => HttpStatusCode.BadRequest,
        ArgumentException => HttpStatusCode.BadRequest,
        InvalidOperationException => HttpStatusCode.BadRequest,
        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
        DbUpdateException => HttpStatusCode.Conflict,
        NotImplementedException => HttpStatusCode.NotImplemented,
        TimeoutException => HttpStatusCode.ServiceUnavailable,
        _ => HttpStatusCode.InternalServerError
      };
    }
    protected ObjectResult GetResult(HttpStatusCode statusCode, string message = "", object? data = null) =>
      StatusCode((int)statusCode, statusCode == HttpStatusCode.NoContent ? null : new ResponseHttp
      {
        StatusCode = (int)statusCode,
        Success = IsSuccessStatusCode(statusCode),
        Message = string.IsNullOrWhiteSpace(message) ? GetMessageByStatusCode(statusCode) : message,
        Data = data
      });
    #region Métodos Private
    private static bool IsSuccessStatusCode(HttpStatusCode statusCode) => (int)statusCode >= 200 && (int)statusCode <= 299;
    private static string GetMessageByStatusCode(HttpStatusCode statusCode)
    {
      return statusCode switch
      {
        HttpStatusCode.OK => "Requisição completa com sucesso.",
        HttpStatusCode.Created => "Registro criado com sucesso.",
        HttpStatusCode.NotFound => "Registro não encontrado.",
        HttpStatusCode.UnprocessableEntity => "Requisição com erro.",
        HttpStatusCode.InternalServerError => "Erro interno de servidor.",
        HttpStatusCode.Unauthorized => "Erro de autorização.",
        _ => ""
      };
    }
    #endregion
  }
}
