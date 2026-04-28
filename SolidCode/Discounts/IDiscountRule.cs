using SolidCode.Domain;

namespace SolidCode.Discounts;

public interface IDiscountRule
{
    decimal Apply(Order order, decimal currentTotal);
}
