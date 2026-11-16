using Data.Class.entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Data.Class.entities.Order;

public class Order : BaseEntity
{
    [Required, MaxLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(320)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string Status { get; set; } = "PendingPayment";

    [Required, MaxLength(3)]
    public string Currency { get; set; } = "usd";

    public long TotalAmountCents { get; set; }

    [MaxLength(255)]
    public string? StripeCheckoutSessionId { get; set; }

    public DateTime? PaidAtUtc { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
