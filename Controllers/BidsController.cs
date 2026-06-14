using AuctionRestApi.DTOs;
using AuctionRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionRestApi.Controllers;

[ApiController]
[Route("auctions/{auctionId}/bids")]
public class BidsController : ControllerBase
{
    private readonly IBidService _bidService;

    public BidsController(IBidService bidService)
    {
        _bidService = bidService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BidReadDto>>> GetByAuctionId(int auctionId)
    {
        var bids = await _bidService.GetByAuctionIdAsync(auctionId);

        if (bids == null)
        {
            return NotFound(new { message = "Auction not found." });
        }

        return Ok(bids);
    }

    [HttpPost]
    public async Task<ActionResult<BidReadDto>> Create(int auctionId, BidCreateDto dto)
    {
        var result = await _bidService.CreateAsync(auctionId, dto);

        if (!result.Success)
        {
            if (result.Error == "Auction not found." || result.Error == "User not found.")
            {
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(nameof(GetByAuctionId), new { auctionId = auctionId }, result.Bid);
    }
}