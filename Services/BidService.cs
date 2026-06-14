using AuctionRestApi.DTOs;
using AuctionRestApi.Models;
using AuctionRestApi.Repositories;

namespace AuctionRestApi.Services;

public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;
    private readonly IAuctionRepository _auctionRepository;
    private readonly IUserRepository _userRepository;

    public BidService(
        IBidRepository bidRepository,
        IAuctionRepository auctionRepository,
        IUserRepository userRepository)
    {
        _bidRepository = bidRepository;
        _auctionRepository = auctionRepository;
        _userRepository = userRepository;
    }

    public async Task<List<BidReadDto>?> GetByAuctionIdAsync(int auctionId)
    {
        var auction = await _auctionRepository.GetByIdAsync(auctionId);

        if (auction == null)
        {
            return null;
        }

        var bids = await _bidRepository.GetByAuctionIdAsync(auctionId);

        return bids.Select(MapToReadDto).ToList();
    }

    public async Task<(bool Success, string? Error, BidReadDto? Bid)> CreateAsync(int auctionId, BidCreateDto dto)
    {
        var auction = await _auctionRepository.GetByIdAsync(auctionId);

        if (auction == null)
        {
            return (false, "Auction not found.", null);
        }

        var user = await _userRepository.GetByIdAsync(dto.UserId);

        if (user == null)
        {
            return (false, "User not found.", null);
        }

        if (auction.Status != AuctionStatus.Active)
        {
            return (false, "Auction is not active.", null);
        }

        if (DateTime.UtcNow < auction.StartDate)
        {
            return (false, "Auction has not started yet.", null);
        }

        if (DateTime.UtcNow > auction.EndDate)
        {
            auction.Status = AuctionStatus.Finished;
            await _auctionRepository.UpdateAsync(auction);

            return (false, "Auction has already ended.", null);
        }

        if (dto.UserId == auction.OwnerId)
        {
            return (false, "Owner cannot bid on own auction.", null);
        }

        if (dto.Amount <= auction.CurrentHighestBid)
        {
            return (false, "Bid amount must be higher than current highest bid.", null);
        }

        var bid = new Bid
        {
            AuctionId = auctionId,
            UserId = dto.UserId,
            Amount = dto.Amount,
            CreatedAt = DateTime.UtcNow
        };

        await _bidRepository.AddAsync(bid);

        auction.CurrentHighestBid = dto.Amount;
        await _auctionRepository.UpdateAsync(auction);

        bid.User = user;

        return (true, null, MapToReadDto(bid));
    }

    private static BidReadDto MapToReadDto(Bid bid)
    {
        return new BidReadDto
        {
            Id = bid.Id,
            AuctionId = bid.AuctionId,
            UserId = bid.UserId,
            Username = bid.User?.Username ?? string.Empty,
            Amount = bid.Amount,
            CreatedAt = bid.CreatedAt
        };
    }
}