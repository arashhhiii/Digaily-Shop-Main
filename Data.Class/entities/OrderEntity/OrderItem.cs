using Data.Class.entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Data.Class.entities.Order;

public class OrderItem : BaseEntity
{
    public long OrderId { get; set; }

    public int ProductId { get; set; }

    [Required, MaxLength(200)]
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public long UnitAmountCents { get; set; }

    public Order Order { get; set; } = null!;
}
