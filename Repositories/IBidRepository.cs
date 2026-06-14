using AuctionRestApi.Models;

namespace AuctionRestApi.Repositories;

public interface IBidRepository
{
    Task<List<Bid>> GetByAuctionIdAsync(int auctionId);
    Task<Bid> AddAsync(Bid bid);
}