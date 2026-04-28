using SolidCode.Domain;

namespace SolidCode.Services;

public class OrderService
{
    private readonly IOrderValidator _validator;
    private readonly IOrderTotalCalculator _calculator;
    private readonly IOrderRepository _repository;
    private readonly IEmailSender _emailSender;

    public OrderService(
        IOrderValidator validator,
        IOrderTotalCalculator calculator,
        IOrderRepository repository,
        IEmailSender emailSender)
    {
        _validator = validator;
        _calculator = calculator;
        _repository = repository;
        _emailSender = emailSender;
    }

    public void ProcessOrder(Order order)
    {
        _validator.Validate(order);

        var total = _calculator.Calculate(order);

        _repository.Save(order, total);

        _emailSender.SendOrderConfirmation(order.CustomerEmail, total);
    }
}
