using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<Client?> GetByIdAsync(int clientId);
    Task<Client> CreateAsync(Client client);
    Task<Client?> UpdateAsync(int clientId, Client client);
    Task<bool> DeleteAsync(int clientId);
    Task<int> GetTotalCountAsync();
}
