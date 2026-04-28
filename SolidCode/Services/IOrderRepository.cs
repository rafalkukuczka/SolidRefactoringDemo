using SolidCode.Domain;

namespace SolidCode.Services;

public interface IOrderRepository
{
    void Save(Order order, decimal total);
}
