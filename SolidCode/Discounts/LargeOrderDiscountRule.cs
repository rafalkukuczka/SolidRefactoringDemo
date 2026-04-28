using SolidCode.Domain;

namespace SolidCode.Discounts;

public class LargeOrderDiscountRule : IDiscountRule
{
    public decimal Apply(Order order, decimal currentTotal)
    {
        return currentTotal > 1000m
            ? currentTotal * 0.9m
            : currentTotal;
    }
}
