using AuctionRestApi.Data;
using AuctionRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionRestApi.Repositories;

public class BidRepository : IBidRepository
{
    private readonly AppDbContext _context;

    public BidRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bid>> GetByAuctionIdAsync(int auctionId)
    {
        return await _context.Bids
            .Include(b => b.User)
            .Where(b => b.AuctionId == auctionId)
            .OrderByDescending(b => b.Amount)
            .ToListAsync();
    }

    public async Task<Bid> AddAsync(Bid bid)
    {
        _context.Bids.Add(bid);
        await _context.SaveChangesAsync();
        return bid;
    }
}