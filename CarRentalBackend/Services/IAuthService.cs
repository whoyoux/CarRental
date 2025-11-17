using CarRentalBackend.Models;
using CarRentalBackend.ModelsDto;

namespace CarRentalBackend.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task EnsureAdminAccountExistsAsync();
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
