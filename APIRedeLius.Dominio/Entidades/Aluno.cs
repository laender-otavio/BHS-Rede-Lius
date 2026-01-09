namespace APIRedeLius.Dominio.Entidades
{
  public class Aluno
  {
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Serie { get; set; }
  }
}