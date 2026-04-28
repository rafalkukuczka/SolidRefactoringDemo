using SolidCode.Domain;

namespace SolidCode.Services;

public interface IOrderTotalCalculator
{
    decimal Calculate(Order order);
}
