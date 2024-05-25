namespace UserManagement.Api.Models;

public class Client
{
    public Guid Id { get; init; }
    public string Nome { get; private set; }
    public string Mail { get; private set; }
    public string Senha { get; private set; }
    private string _cpf;
    public string Cpf
    {
        get => _cpf;
        private set
        {
            if (!Validations.CPFValidation.IsValid(value))
                throw new ArgumentException("CPF inválido");
            _cpf = value;
        }
    }
    // public DateTime Nasc { get; private set; }

    public Client(string cpf, string nome, string mail, string senha)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Mail = mail;
        Cpf = cpf;
        Senha = senha;
        // Nasc = nasc.Date;
    }

    public void dataUpdate(string cpf, string nome, string mail, string senha)
    {
        Nome = nome;
        Mail = mail;
        Cpf = cpf;
        Senha = senha;
    }
}