using Domain.SharedKernel;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using FluentAssertions;

namespace Domain.Tests.ShraredKernel;

public class MoneyTests
{
    [Fact]
    public void Cannot_create_money_with_zero_value()
    {
        var money = Money.Create(0m, CurrencyType.SGD);

        money.IsFailure.Should().Be(true);
        money.Error.Should().Be(DomainErrors.Money.InvalidMoney);
    }
    
    [Fact]
    public void Cannot_create_money_with_negative_value()
    {
        var money = Money.Create(-1m, CurrencyType.SGD);

        money.IsFailure.Should().Be(true);
        money.Error.Should().Be(DomainErrors.Money.InvalidMoney);
    }
    
    [Fact]
    public void Cannot_create_money_with_decimal_value_when_currency_type_is_vnd()
    {
        var money = Money.Create(1.5m, CurrencyType.VND);

        money.IsFailure.Should().Be(true);
        money.Error.Should().Be(DomainErrors.Money.InvalidVNDCurrencyMoney);
    }
    
    [Fact]
    public void Create_money_with_vnd_currency_successfully()
    {
        var money = Money.Create(500m, CurrencyType.VND);

        money.IsSuccess.Should().Be(true);
        money.Value.Value.Should().Be(500m);
        money.Value.CurrencyType.Should().Be(CurrencyType.VND);
    }
    
    [Fact]
    public void Create_money_successfully()
    {
        var money = Money.Create(1.5m, CurrencyType.USD);

        money.IsSuccess.Should().Be(true);
        money.Value.Value.Should().Be(1.5m);
        money.Value.CurrencyType.Should().Be(CurrencyType.USD);
    }

    [Fact]
    public void Two_money_instances_equal_if_be_the_same_value_and_currency_type()
    {
        var money1 = Money.Create(200, CurrencyType.USD).Value;
        var money2 = Money.Create(200, CurrencyType.USD).Value;

        money1.Should().Be(money2);
        money1.GetHashCode().Should().Be(money2.GetHashCode());
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_be_different_value()
    {
        var money1 = Money.Create(300m, CurrencyType.USD).Value;
        var money2 = Money.Create(200m, CurrencyType.USD).Value;

        money1.Should().NotBe(money2);
        money1.GetHashCode().Should().NotBe(money2.GetHashCode());
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_be_different_currency_type()
    {
        var money1 = Money.Create(200m, CurrencyType.USD).Value;
        var money2 = Money.Create(200m, CurrencyType.VND).Value;

        money1.Should().NotBe(money2);
        money1.GetHashCode().Should().NotBe(money2.GetHashCode());
    }
}