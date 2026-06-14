using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace AuctionRestApi.Models;

public class Auction
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal StartingPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentHighestBid { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public AuctionStatus Status { get; set; } = AuctionStatus.Active;

    public int OwnerId { get; set; }

    public User? Owner { get; set; }

    public List<Bid> Bids { get; set; } = new();
}