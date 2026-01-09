using System.Net.Mail;

namespace APIRedeLius.Dominio.Utils
{
  public class Funcoes
  {
    public static bool StringValida(string s)
    {
      return !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s);
    }
    public static bool TamanhoValido(string s, int tamanho)
    {
      if (!StringValida(s))
        return false;

      if (s.Length > tamanho)
        return false;

      return true;
    }
    public static bool EmailValido(string email)
    {
      try
      {
        var addr = new MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }
  }
}
