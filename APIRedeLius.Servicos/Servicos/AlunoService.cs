using APIRedeLius.Dominio.Entidades;
using APIRedeLius.Dominio.Exceptions;
using APIRedeLius.Dominio.Interfaces;
using APIRedeLius.Dominio.Utils;
using APIRedeLius.Servicos.DTOs;
using APIRedeLius.Servicos.Interfaces;
using System.Text;

namespace APIRedeLius.Servicos.Servicos
{
  public class AlunoService : IAlunoService
  {
    private readonly IAlunoRepository _alunoRepository;
    public AlunoService(IAlunoRepository alunoRepository)
    {
      _alunoRepository = alunoRepository;
    }
    public async Task<Aluno> CriarAluno(AlunoDTO aluno)
    {
      var validacoes = new StringBuilder();

      if (!Funcoes.StringValida(aluno.Nome))
        validacoes.AppendLine("O nome do aluno é obrigatório.");

      if (!Funcoes.TamanhoValido(aluno.Nome, 100))
        validacoes.AppendLine("O nome do aluno pode ter no máximo 100 caracteres.");

      if (!Funcoes.StringValida(aluno.Email))
        validacoes.AppendLine("O email do aluno é obrigatório.");

      if (!Funcoes.TamanhoValido(aluno.Email, 100))
        validacoes.AppendLine("O email do aluno pode ter no máximo 100 caracteres.");

      if (!Funcoes.EmailValido(aluno.Email))
        validacoes.AppendLine("O email do aluno está inválido.");

      if (!Funcoes.StringValida(aluno.Serie))
        validacoes.AppendLine("A série do aluno é obrigatório.");

      if (!Funcoes.TamanhoValido(aluno.Serie, 100))
        validacoes.AppendLine("A série do aluno pode ter no máximo 100 caracteres.");

      if (validacoes.Length > 0)
        throw new InvalidOperationException(validacoes.ToString());

      return await _alunoRepository.Add(new Aluno
      {
        Nome = aluno.Nome,
        Email = aluno.Email,
        Serie = aluno.Serie
      });
    }
    public async Task<List<Aluno>> RetornarAlunos()
    {
      var alunos = await _alunoRepository.Select();
      return alunos.ToList();
    }
    public async Task<Aluno> RetornarAluno(Guid id)
    {
      return await _alunoRepository.SelectSingle(x => x.Id == id) ?? throw new NotFoundException($"Aluno com o ID {id} não encontrado.");
    }
    public async Task<string> DeletarAluno(Guid id)
    {
      var aluno = await RetornarAluno(id);
      await _alunoRepository.Delete(aluno.Id);
      return $"Aluno com o ID {id} deletado com sucesso.";
    }
  }
}
