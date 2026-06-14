using AuctionRestApi.DTOs;
using AuctionRestApi.Models;
using AuctionRestApi.Repositories;

namespace AuctionRestApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserReadDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(MapToReadDto).ToList();
    }

    public async Task<UserReadDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        return MapToReadDto(user);
    }

    public async Task<(bool Success, string? Error, UserReadDto? User)> CreateAsync(UserCreateDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            return (false, "User with this email already exists.", null);
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        return (true, null, MapToReadDto(user));
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return (false, "User not found.");
        }

        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null && existingUser.Id != id)
        {
            return (false, "User with this email already exists.");
        }

        user.Username = dto.Username;
        user.Email = dto.Email;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;

        await _userRepository.UpdateAsync(user);

        return (true, null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private static UserReadDto MapToReadDto(User user)
    {
        return new UserReadDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt
        };
    }
}