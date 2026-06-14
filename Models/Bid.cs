using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionRestApi.Models;

public class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public Auction? Auction { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}