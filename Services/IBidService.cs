using AuctionRestApi.DTOs;

namespace AuctionRestApi.Services;

public interface IBidService
{
    Task<List<BidReadDto>?> GetByAuctionIdAsync(int auctionId);
    Task<(bool Success, string? Error, BidReadDto? Bid)> CreateAsync(int auctionId, BidCreateDto dto);
}