using AuctionRestApi.DTOs;
using AuctionRestApi.Models;

namespace AuctionRestApi.Services;

public interface IAuctionService
{
    Task<List<AuctionReadDto>> GetAllAsync(string? category, AuctionStatus? status, int page, int pageSize, string? sortBy);
    Task<AuctionReadDto?> GetByIdAsync(int id);
    Task<(bool Success, string? Error, AuctionReadDto? Auction)> CreateAsync(AuctionCreateDto dto);
    Task<(bool Success, string? Error)> UpdateAsync(int id, AuctionUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}