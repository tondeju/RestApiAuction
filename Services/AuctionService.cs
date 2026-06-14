using AuctionRestApi.DTOs;
using AuctionRestApi.Models;
using AuctionRestApi.Repositories;

namespace AuctionRestApi.Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IUserRepository _userRepository;

    public AuctionService(IAuctionRepository auctionRepository, IUserRepository userRepository)
    {
        _auctionRepository = auctionRepository;
        _userRepository = userRepository;
    }

    public async Task<List<AuctionReadDto>> GetAllAsync(string? category, AuctionStatus? status, int page, int pageSize, string? sortBy)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        if (pageSize > 50)
        {
            pageSize = 50;
        }

        var auctions = await _auctionRepository.GetAllAsync(category, status, page, pageSize, sortBy);

        return auctions.Select(MapToReadDto).ToList();
    }

    public async Task<AuctionReadDto?> GetByIdAsync(int id)
    {
        var auction = await _auctionRepository.GetByIdAsync(id);

        if (auction == null)
        {
            return null;
        }

        return MapToReadDto(auction);
    }

    public async Task<(bool Success, string? Error, AuctionReadDto? Auction)> CreateAsync(AuctionCreateDto dto)
    {
        var owner = await _userRepository.GetByIdAsync(dto.OwnerId);

        if (owner == null)
        {
            return (false, "Owner user not found.", null);
        }

        if (dto.EndDate <= dto.StartDate)
        {
            return (false, "End date must be later than start date.", null);
        }

        var auction = new Auction
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            StartingPrice = dto.StartingPrice,
            CurrentHighestBid = dto.StartingPrice,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            OwnerId = dto.OwnerId,
            Status = AuctionStatus.Active
        };

        await _auctionRepository.AddAsync(auction);

        auction.Owner = owner;

        return (true, null, MapToReadDto(auction));
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, AuctionUpdateDto dto)
    {
        var auction = await _auctionRepository.GetByIdAsync(id);

        if (auction == null)
        {
            return (false, "Auction not found.");
        }

        if (dto.EndDate <= dto.StartDate)
        {
            return (false, "End date must be later than start date.");
        }

        if (dto.StartingPrice > auction.CurrentHighestBid)
        {
            return (false, "Starting price cannot be higher than current highest bid.");
        }

        auction.Title = dto.Title;
        auction.Description = dto.Description;
        auction.Category = dto.Category;
        auction.StartingPrice = dto.StartingPrice;
        auction.StartDate = dto.StartDate;
        auction.EndDate = dto.EndDate;
        auction.Status = dto.Status;

        await _auctionRepository.UpdateAsync(auction);

        return (true, null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _auctionRepository.DeleteAsync(id);
    }

    private static AuctionReadDto MapToReadDto(Auction auction)
    {
        return new AuctionReadDto
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            Category = auction.Category,
            StartingPrice = auction.StartingPrice,
            CurrentHighestBid = auction.CurrentHighestBid,
            StartDate = auction.StartDate,
            EndDate = auction.EndDate,
            Status = auction.Status,
            OwnerId = auction.OwnerId,
            OwnerUsername = auction.Owner?.Username ?? string.Empty
        };
    }
}