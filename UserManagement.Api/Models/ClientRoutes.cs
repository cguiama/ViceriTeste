using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Data;
namespace UserManagement.Api.Models;

public static class ClientRoutes
{
    public static void AddClientsRouts(this WebApplication app)
    {

        var clientRoutes = app.MapGroup("clients");
        
        //criação de cliente
        clientRoutes.MapPost("", async (AddClientRequest request, AppDbContext context, CancellationToken ct) =>
        {
            var existingCpf = await context.Clients.AnyAsync(client => client.Cpf == request.Cpf, ct);
            var existingEmail = await context.Clients.AnyAsync(client => client.Mail == request.Mail, ct);
            //verificação de duplicatas
            if (existingCpf)
                return Results.Conflict("CPF já cadastrado");
            if (existingEmail)
                return Results.Conflict("Email já cadastrado");
            
            var newClient = new Client(request.Cpf, request.Nome, request.Mail, request.Senha);
            await context.Clients.AddAsync(newClient, ct);
            //salva as mudanças que fiz no context
            await context.SaveChangesAsync(ct);
            //esconder dados sensiveis

            var clientReturn = new ClientsDto(newClient.Id, newClient.Nome, newClient.Mail);
            
            return Results.Ok(clientReturn);
        });
        
        //get lista de clientes cadastrados
        clientRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
        {
            var clients = await context.Clients
            .Select(client => new ClientsDto(client.Id, client.Nome, client.Mail)).ToListAsync(ct);
            return clients;
        });
        
        //Atualizar Dados buscando por ID
        clientRoutes.MapPut("{id}", async (Guid id, UpdateClientRequest request, AppDbContext context, CancellationToken ct) =>
        {
            
            var existingCpf = await context.Clients.AnyAsync(client => client.Cpf == request.Cpf, ct);
            var existingEmail = await context.Clients.AnyAsync(client => client.Mail == request.Mail, ct);
            var client = await context.Clients.SingleOrDefaultAsync(client => client.Id == id, ct);
            

            if (client == null)
                return Results.Conflict("Cliente não encontrado");
            //verificação de duplicatas
            if (existingCpf)
                return Results.Conflict("CPF já cadastrado");
            if (existingEmail)
                return Results.Conflict("Email já cadastrado");
            
            //Preferi criar um endpoint que altere todos os dados, mas tenho ciência que posso criar um para cada.
            client.dataUpdate(request.Cpf, request.Nome, request.Mail, request.Senha);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new ClientsDto(client.Id, client.Nome, client.Mail));
        });
        
        //Deletar Dados
        clientRoutes.MapDelete("{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
        {
            var client = await context.Clients.SingleOrDefaultAsync(client => client.Id == id, ct);

            if (client == null)
                return Results.Conflict("Cliente não encontrado");
            context.Clients.Remove(client);
            await context.SaveChangesAsync(ct);
            return Results.Ok("Cliente foi deletado com sucesso!");
        });
    }
}