using APIRedeLius.Dominio.DTOs;
using APIRedeLius.Servicos.DTOs;
using APIRedeLius.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIRedeLius.API.Controllers
{
  [Route("api/Alunos")]
  public class AlunoController : BaseController
  {
    private readonly IAlunoService _service;
    public AlunoController(IAlunoService service) => _service = service;
    /// <summary>
    /// Endpoint que cria um aluno
    /// </summary>
    /// <param name="aluno"></param>
    [Authorize("Bearer")]
    [HttpPost]
    public async Task<ActionResult<ResponseHttp>> CriarAluno([FromBody] AlunoDTO aluno)
    {
      try
      {
        return GetResult(HttpStatusCode.OK, string.Empty, await _service.CriarAluno(aluno));
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
    /// <summary>
    /// Endpoint que retorna os alunos
    /// </summary>
    [Authorize("Bearer")]
    [HttpGet]
    public async Task<ActionResult<ResponseHttp>> RetornarAlunos()
    {
      try
      {
        return GetResult(HttpStatusCode.OK, string.Empty, await _service.RetornarAlunos());
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
    /// <summary>
    /// Endpoint que retorna o aluno por ID
    /// </summary>
    /// <param name="id"></param>
    [Authorize("Bearer")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseHttp>> RetornarAluno(string id)
    {
      try
      {
        if (!Guid.TryParse(id, out var guid))
          throw new FormatException("Id inválido. Informe um GUID válido.");

        return GetResult(HttpStatusCode.OK, string.Empty, await _service.RetornarAluno(guid));
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
    /// <summary>
    /// Endpoint que deleta o aluno por ID
    /// </summary>
    [Authorize("Bearer")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseHttp>> ExcluirAluno(string id)
    {
      try
      {
        if (!Guid.TryParse(id, out var guid))
          throw new FormatException("Id inválido. Informe um GUID válido.");

        return GetResult(HttpStatusCode.OK, string.Empty, await _service.DeletarAluno(guid));
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
  }
}
