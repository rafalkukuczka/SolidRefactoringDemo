namespace SolidCode.Domain;

public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = "";
    public bool IsVipCustomer { get; set; }
    public string? CouponCode { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}
