using APIRedeLius.Dominio.DTOs;
using APIRedeLius.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIRedeLius.API.Controllers
{
  public class AutenticacaoController : BaseController
  {
    private readonly IAutenticacaoService _autenticacaoService;
    public AutenticacaoController(IAutenticacaoService service) => _autenticacaoService = service;
    /// <summary>
    /// API que autentica login e senha e retorna token de acesso aos outros Endpoints
    /// </summary>
    /// <param name="tipoAcesso"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    public ActionResult<ResponseHttp> Autenticar()
    {
      try
      {
        return GetResult(HttpStatusCode.OK, string.Empty, _autenticacaoService.Autenticar());
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
  }
}
