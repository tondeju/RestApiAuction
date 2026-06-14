using AuctionRestApi.DTOs;

namespace AuctionRestApi.Services;

public interface IUserService
{
    Task<List<UserReadDto>> GetAllAsync();
    Task<UserReadDto?> GetByIdAsync(int id);
    Task<(bool Success, string? Error, UserReadDto? User)> CreateAsync(UserCreateDto dto);
    Task<(bool Success, string? Error, UserReadDto? User)> RegisterAsync(RegisterDto dto);
    Task<(bool Success, string? Error, LoginResponseDto? Response)> LoginAsync(LoginDto dto);
    Task<(bool Success, string? Error)> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}