using FluentAssertions;
using Moq;
using SolidCode.Discounts;
using SolidCode.Domain;
using SolidCode.Services;
using Xunit;

namespace SolidCode.Tests;

public class OrderTotalCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnSumWithoutDiscounts()
    {
        var calculator = new OrderTotalCalculator(Array.Empty<IDiscountRule>());

        var order = new Order
        {
            Items =
            {
                new OrderItem { ProductName = "Gateway", UnitPrice = 500m, Quantity = 2 },
                new OrderItem { ProductName = "License", UnitPrice = 100m, Quantity = 1 }
            }
        };

        var total = calculator.Calculate(order);

        total.Should().Be(1100m);
    }

    [Fact]
    public void Calculate_ShouldApplyLargeOrderDiscount()
    {
        var calculator = new OrderTotalCalculator(new IDiscountRule[]
        {
            new LargeOrderDiscountRule()
        });

        var order = new Order
        {
            Items =
            {
                new OrderItem { ProductName = "Gateway", UnitPrice = 1200m, Quantity = 1 }
            }
        };

        var total = calculator.Calculate(order);

        total.Should().Be(1080m);
    }

    [Fact]
    public void Calculate_ShouldCallDiscountRuleWithCurrentTotal()
    {
        var order = new Order
        {
            Items =
            {
                new OrderItem { ProductName = "Gateway", UnitPrice = 1000m, Quantity = 1 }
            }
        };

        var discountRuleMock = new Mock<IDiscountRule>();

        discountRuleMock
            .Setup(x => x.Apply(order, 1000m))
            .Returns(900m);

        var calculator = new OrderTotalCalculator(new[] { discountRuleMock.Object });

        var total = calculator.Calculate(order);

        total.Should().Be(900m);

        discountRuleMock.Verify(
            x => x.Apply(order, 1000m),
            Times.Once);
    }
}
