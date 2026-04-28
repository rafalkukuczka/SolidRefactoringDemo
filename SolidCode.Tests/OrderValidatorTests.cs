using FluentAssertions;
using SolidCode.Domain;
using SolidCode.Services;
using Xunit;

namespace SolidCode.Tests;

public class OrderValidatorTests
{
    [Fact]
    public void Validate_WhenEmailIsEmpty_ShouldThrowException()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            CustomerEmail = "",
            Items =
            {
                new OrderItem { ProductName = "Gateway", UnitPrice = 100m, Quantity = 1 }
            }
        };

        var action = () => validator.Validate(order);

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Customer email is required.");
    }

    [Fact]
    public void Validate_WhenOrderIsCorrect_ShouldNotThrow()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            CustomerEmail = "customer@example.com",
            Items =
            {
                new OrderItem { ProductName = "Gateway", UnitPrice = 100m, Quantity = 1 }
            }
        };

        var action = () => validator.Validate(order);

        action.Should().NotThrow();
    }
}
