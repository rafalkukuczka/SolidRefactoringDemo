using SolidCode.Domain;

namespace SolidCode.Services;

public class OrderValidator : IOrderValidator
{
    public void Validate(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (string.IsNullOrWhiteSpace(order.CustomerEmail))
        {
            throw new InvalidOperationException("Customer email is required.");
        }

        if (order.Items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain at least one item.");
        }

        foreach (var item in order.Items)
        {
            if (item.Quantity <= 0)
            {
                throw new InvalidOperationException("Quantity must be greater than zero.");
            }

            if (item.UnitPrice < 0)
            {
                throw new InvalidOperationException("Unit price cannot be negative.");
            }
        }
    }
}
