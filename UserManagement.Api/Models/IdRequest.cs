namespace UserManagement.Api.Models;

public record IdRequest(Guid Id, string Cpf, string Nome, string Mail, DateTime Nasc);