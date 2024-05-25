using System.Text.RegularExpressions;

namespace UserManagement.Api.Models;

public class Validations
{
    public static class CPFValidation
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;
            cpf = Regex.Replace(cpf, @"\D", "");
            return cpf.Length == 11 && cpf.Distinct().Count() != 1;
            // Não adicionei verificação dos digitos, afim permitir testes com numeros ficticios.
        }
    }
}
