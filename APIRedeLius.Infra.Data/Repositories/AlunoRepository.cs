using APIRedeLius.Dominio.Entidades;
using APIRedeLius.Dominio.Interfaces;
using APIRedeLius.Infra.Data.Contextos;

namespace APIRedeLius.Infra.Data.Repositories
{
  public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
  {
    public AlunoRepository(Contexto contexto) : base(contexto)
    {
    }
  }
}