using APIRedeLius.Dominio.Entidades;
using APIRedeLius.Servicos.DTOs;

namespace APIRedeLius.Servicos.Interfaces
{
  public interface IAlunoService
  {
    Task<Aluno> CriarAluno(AlunoDTO aluno);
    Task<List<Aluno>> RetornarAlunos();
    Task<Aluno> RetornarAluno(Guid id);
    Task<string> DeletarAluno(Guid id);
  }
}
