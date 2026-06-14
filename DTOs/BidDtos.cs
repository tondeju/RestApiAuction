using System.ComponentModel.DataAnnotations;

namespace AuctionRestApi.DTOs;

public class BidCreateDto
{
    [Required]
    public int UserId { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}

public class BidReadDto
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}