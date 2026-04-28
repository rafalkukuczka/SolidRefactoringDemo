using SolidCode.Discounts;
using SolidCode.Domain;
using SolidCode.Infrastructure;
using SolidCode.Services;

var order = new Order
{
    Id = 1001,
    CustomerEmail = "customer@example.com",
    IsVipCustomer = false,
    CouponCode = null,
    Items =
    {
        new OrderItem
        {
            ProductName = "Industrial Gateway",
            UnitPrice = 1200m,
            Quantity = 1
        },
        new OrderItem
        {
            ProductName = "Support Package",
            UnitPrice = 100m,
            Quantity = 1
        }
    }
};

// Composition root for console application.
// In ASP.NET Core this would usually be configured in Program.cs with dependency injection.
var discountRules = new IDiscountRule[]
{
    new LargeOrderDiscountRule(),
    new VipCustomerDiscountRule(),
    new CouponDiscountRule()
};

IOrderValidator validator = new OrderValidator();
IOrderTotalCalculator calculator = new OrderTotalCalculator(discountRules);
IOrderRepository repository = new ConsoleOrderRepository();
IEmailSender emailSender = new ConsoleEmailSender();

var service = new OrderService(
    validator,
    calculator,
    repository,
    emailSender);

service.ProcessOrder(order);

Console.WriteLine();
Console.WriteLine("SOLID order processing finished.");
