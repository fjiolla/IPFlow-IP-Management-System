using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDTO?> RegisterAsync(RegisterDTO dto);
    Task<AuthResponseDTO?> LoginAsync(LoginDTO dto);
    string GenerateJwtToken(int userId, string email, string role);
}
