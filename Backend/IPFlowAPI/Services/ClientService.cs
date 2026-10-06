using IPFlowAPI.DTOs;
using IPFlowAPI.Models;
using IPFlowAPI.Repositories;

namespace IPFlowAPI.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<(IEnumerable<ClientDTO>, int)> GetAllAsync(int pageNumber, int pageSize)
    {
        var clients = await _clientRepository.GetAllAsync(pageNumber, pageSize);
        var totalCount = await _clientRepository.GetTotalCountAsync();

        var clientDTOs = clients.Select(c => new ClientDTO
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
            Address = c.Address,
            CreatedAt = c.CreatedAt
        });

        return (clientDTOs, totalCount);
    }

    public async Task<ClientDTO?> GetByIdAsync(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null) return null;

        return new ClientDTO
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            Address = client.Address,
            CreatedAt = client.CreatedAt
        };
    }

    public async Task<ClientDTO> CreateAsync(CreateClientDTO dto)
    {
        var client = new Client
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address
        };

        var createdClient = await _clientRepository.CreateAsync(client);

        return new ClientDTO
        {
            ClientId = createdClient.ClientId,
            Name = createdClient.Name,
            Email = createdClient.Email,
            Phone = createdClient.Phone,
            Address = createdClient.Address,
            CreatedAt = createdClient.CreatedAt
        };
    }

    public async Task<ClientDTO?> UpdateAsync(int clientId, UpdateClientDTO dto)
    {
        var existingClient = await _clientRepository.GetByIdAsync(clientId);
        if (existingClient == null) return null;

        if (!string.IsNullOrEmpty(dto.Name))
            existingClient.Name = dto.Name;
        if (!string.IsNullOrEmpty(dto.Email))
            existingClient.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.Phone))
            existingClient.Phone = dto.Phone;
        if (!string.IsNullOrEmpty(dto.Address))
            existingClient.Address = dto.Address;

        var updatedClient = await _clientRepository.UpdateAsync(clientId, existingClient);
        if (updatedClient == null) return null;

        return new ClientDTO
        {
            ClientId = updatedClient.ClientId,
            Name = updatedClient.Name,
            Email = updatedClient.Email,
            Phone = updatedClient.Phone,
            Address = updatedClient.Address,
            CreatedAt = updatedClient.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int clientId)
    {
        return await _clientRepository.DeleteAsync(clientId);
    }
}
