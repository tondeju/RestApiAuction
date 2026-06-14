using AuctionRestApi.DTOs;
using AuctionRestApi.Models;
using AuctionRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionRestApi.Controllers;

[ApiController]
[Route("auctions")]
public class AuctionsController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionsController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionReadDto>>> GetAll(
        [FromQuery] string? category,
        [FromQuery] AuctionStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null)
    {
        var auctions = await _auctionService.GetAllAsync(category, status, page, pageSize, sortBy);

        return Ok(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionReadDto>> GetById(int id)
    {
        var auction = await _auctionService.GetByIdAsync(id);

        if (auction == null)
        {
            return NotFound(new { message = "Auction not found." });
        }

        return Ok(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionReadDto>> Create(AuctionCreateDto dto)
    {
        var result = await _auctionService.CreateAsync(dto);

        if (!result.Success)
        {
            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Auction!.Id }, result.Auction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AuctionUpdateDto dto)
    {
        var result = await _auctionService.UpdateAsync(id, dto);

        if (!result.Success)
        {
            if (result.Error == "Auction not found.")
            {
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _auctionService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Auction not found." });
        }

        return NoContent();
    }
}