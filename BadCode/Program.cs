using System.Net.Mail;

var order = new Order
{
    Id = 1,
    CustomerEmail = "customer@example.com",
    Items =
    {
        new OrderItem { ProductName = "Industrial Gateway", UnitPrice = 1200m, Quantity = 1 }
    }
};

var service = new OrderService();
service.ProcessOrder(order);

public class OrderItem
{
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = "";
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderService
{
    public void ProcessOrder(Order order)
    {
        decimal total = 0;

        foreach (var item in order.Items)
        {
            total += item.UnitPrice * item.Quantity;
        }

        if (total > 1000)
        {
            total *= 0.9m;
        }

        Console.WriteLine($"Saving order {order.Id} with total {total}");

        // Bad design: concrete SMTP dependency inside business service.
        // Commented out to avoid sending real email during demo execution.
        // var smtp = new SmtpClient("smtp.company.com");
        // smtp.Send("sales@company.com", order.CustomerEmail, "Order confirmation", $"Total: {total}");

        Console.WriteLine("Email sent.");
    }
}
