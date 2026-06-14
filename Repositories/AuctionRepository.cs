using AuctionRestApi.Data;
using AuctionRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionRestApi.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly AppDbContext _context;

    public AuctionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Auction>> GetAllAsync(string? category, AuctionStatus? status, int page, int pageSize, string? sortBy)
    {
        var query = _context.Auctions
            .Include(a => a.Owner)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(a => a.Category.ToLower() == category.ToLower());
        }

        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }

        query = sortBy?.ToLower() switch
        {
            "title" => query.OrderBy(a => a.Title),
            "price" => query.OrderBy(a => a.CurrentHighestBid),
            "enddate" => query.OrderBy(a => a.EndDate),
            _ => query.OrderBy(a => a.Id)
        };

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Auction?> GetByIdAsync(int id)
    {
        return await _context.Auctions
            .Include(a => a.Owner)
            .Include(a => a.Bids)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Auction> AddAsync(Auction auction)
    {
        _context.Auctions.Add(auction);
        await _context.SaveChangesAsync();
        return auction;
    }

    public async Task<bool> UpdateAsync(Auction auction)
    {
        _context.Auctions.Update(auction);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var auction = await _context.Auctions.FirstOrDefaultAsync(a => a.Id == id);

        if (auction == null)
        {
            return false;
        }

        _context.Auctions.Remove(auction);
        return await _context.SaveChangesAsync() > 0;
    }
}