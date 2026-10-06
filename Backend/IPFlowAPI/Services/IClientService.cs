using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface IClientService
{
    Task<(IEnumerable<ClientDTO>, int)> GetAllAsync(int pageNumber, int pageSize);
    Task<ClientDTO?> GetByIdAsync(int clientId);
    Task<ClientDTO> CreateAsync(CreateClientDTO dto);
    Task<ClientDTO?> UpdateAsync(int clientId, UpdateClientDTO dto);
    Task<bool> DeleteAsync(int clientId);
}
