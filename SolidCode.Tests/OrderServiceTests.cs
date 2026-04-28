using FluentAssertions;
using Moq;
using SolidCode.Domain;
using SolidCode.Services;
using Xunit;

namespace SolidCode.Tests;

public class OrderServiceTests
{
    [Fact]
    public void ProcessOrder_ShouldValidateCalculateSaveAndSendEmail()
    {
        var order = new Order
        {
            Id = 10,
            CustomerEmail = "customer@example.com",
            Items =
            {
                new OrderItem { ProductName = "PLC Adapter", UnitPrice = 1200m, Quantity = 1 }
            }
        };

        var validatorMock = new Mock<IOrderValidator>();
        var calculatorMock = new Mock<IOrderTotalCalculator>();
        var repositoryMock = new Mock<IOrderRepository>();
        var emailSenderMock = new Mock<IEmailSender>();

        calculatorMock
            .Setup(x => x.Calculate(order))
            .Returns(1080m);

        var service = new OrderService(
            validatorMock.Object,
            calculatorMock.Object,
            repositoryMock.Object,
            emailSenderMock.Object);

        service.ProcessOrder(order);

        validatorMock.Verify(x => x.Validate(order), Times.Once);
        calculatorMock.Verify(x => x.Calculate(order), Times.Once);
        repositoryMock.Verify(x => x.Save(order, 1080m), Times.Once);
        emailSenderMock.Verify(x => x.SendOrderConfirmation("customer@example.com", 1080m), Times.Once);
    }

    [Fact]
    public void ProcessOrder_WhenValidationFails_ShouldNotCalculateSaveOrSendEmail()
    {
        var order = new Order
        {
            CustomerEmail = "",
            Items =
            {
                new OrderItem { ProductName = "PLC Adapter", UnitPrice = 1200m, Quantity = 1 }
            }
        };

        var validatorMock = new Mock<IOrderValidator>();
        var calculatorMock = new Mock<IOrderTotalCalculator>();
        var repositoryMock = new Mock<IOrderRepository>();
        var emailSenderMock = new Mock<IEmailSender>();

        validatorMock
            .Setup(x => x.Validate(order))
            .Throws(new InvalidOperationException("Customer email is required."));

        var service = new OrderService(
            validatorMock.Object,
            calculatorMock.Object,
            repositoryMock.Object,
            emailSenderMock.Object);

        var action = () => service.ProcessOrder(order);

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Customer email is required.");

        calculatorMock.Verify(x => x.Calculate(It.IsAny<Order>()), Times.Never);
        repositoryMock.Verify(x => x.Save(It.IsAny<Order>(), It.IsAny<decimal>()), Times.Never);
        emailSenderMock.Verify(x => x.SendOrderConfirmation(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
    }
}
