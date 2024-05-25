namespace UserManagement.Api.Models;

public record AddClientRequest(string Cpf, string Nome, string Mail, string Senha);