using SolidCode.Domain;

namespace SolidCode.Discounts;

public class CouponDiscountRule : IDiscountRule
{
    public decimal Apply(Order order, decimal currentTotal)
    {
        return order.CouponCode == "SAVE20"
            ? currentTotal * 0.8m
            : currentTotal;
    }
}
