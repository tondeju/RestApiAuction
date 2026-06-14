using AuctionRestApi.Models;

namespace AuctionRestApi.Repositories;

public interface IAuctionRepository
{
    Task<List<Auction>> GetAllAsync(string? category, AuctionStatus? status, int page, int pageSize, string? sortBy);
    Task<Auction?> GetByIdAsync(int id);
    Task<Auction> AddAsync(Auction auction);
    Task<bool> UpdateAsync(Auction auction);
    Task<bool> DeleteAsync(int id);
}