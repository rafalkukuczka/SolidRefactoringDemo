using SolidCode.Domain;

namespace SolidCode.Discounts;

public class VipCustomerDiscountRule : IDiscountRule
{
    public decimal Apply(Order order, decimal currentTotal)
    {
        return order.IsVipCustomer
            ? currentTotal * 0.95m
            : currentTotal;
    }
}
