using SolidCode.Domain;

namespace SolidCode.Services;

public interface IOrderValidator
{
    void Validate(Order order);
}
