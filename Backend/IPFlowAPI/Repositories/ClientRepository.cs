using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Data;
using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _context.Clients
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int clientId)
    {
        return await _context.Clients
            .Include(c => c.Patents)
            .Include(c => c.Trademarks)
            .Include(c => c.Cases)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(client.ClientId) ?? client;
    }

    public async Task<Client?> UpdateAsync(int clientId, Client client)
    {
        var existingClient = await _context.Clients.FindAsync(clientId);
        if (existingClient == null) return null;

        existingClient.Name = client.Name;
        existingClient.Email = client.Email;
        existingClient.Phone = client.Phone;
        existingClient.Address = client.Address;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(clientId);
    }

    public async Task<bool> DeleteAsync(int clientId)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Clients.CountAsync();
    }
}
