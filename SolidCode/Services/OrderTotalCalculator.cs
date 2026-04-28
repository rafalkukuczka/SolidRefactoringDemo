using SolidCode.Discounts;
using SolidCode.Domain;

namespace SolidCode.Services;

public class OrderTotalCalculator : IOrderTotalCalculator
{
    private readonly IEnumerable<IDiscountRule> _discountRules;

    public OrderTotalCalculator(IEnumerable<IDiscountRule> discountRules)
    {
        _discountRules = discountRules;
    }

    public decimal Calculate(Order order)
    {
        decimal total = 0m;

        foreach (var item in order.Items)
        {
            total += item.UnitPrice * item.Quantity;
        }

        foreach (var rule in _discountRules)
        {
            total = rule.Apply(order, total);
        }

        return total;
    }
}
