using System.ComponentModel.DataAnnotations;
using AuctionRestApi.Models;

namespace AuctionRestApi.DTOs;

public class AuctionCreateDto
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal StartingPrice { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int OwnerId { get; set; }
}

public class AuctionUpdateDto
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal StartingPrice { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public AuctionStatus Status { get; set; }
}

public class AuctionReadDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal StartingPrice { get; set; }

    public decimal CurrentHighestBid { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public AuctionStatus Status { get; set; }

    public int OwnerId { get; set; }

    public string OwnerUsername { get; set; } = string.Empty;
}