using SolidCode.Domain;
using SolidCode.Services;

namespace SolidCode.Infrastructure;

public class ConsoleOrderRepository : IOrderRepository
{
    public void Save(Order order, decimal total)
    {
        Console.WriteLine($"Order {order.Id} saved. Total: {total:0.00}");
    }
}
