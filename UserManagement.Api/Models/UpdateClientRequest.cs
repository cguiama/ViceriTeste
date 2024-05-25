namespace UserManagement.Api.Models;

public record UpdateClientRequest(string Cpf, string Nome, string Mail, string Senha);