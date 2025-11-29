using SharedKernel.Enumerations;
using SharedKernel.ValueObjects;
using Shouldly;
using MoneyDomainErrors = SharedKernel.ValueObjects.DomainErrors;

namespace SharedKernel.Tests;

public sealed class MoneyTests
{
    [Fact]
    public void Cannot_create_money_with_zero_value()
    {
        var money = Money.Create(0m, CurrencyType.SGD);

        money.IsSuccess.ShouldBeFalse();
        money.Error.ShouldBe(MoneyDomainErrors.Money.InvalidMoney);
    }
    
    [Fact]
    public void Cannot_create_money_with_negative_value()
    {
        var money = Money.Create(-1m, CurrencyType.SGD);

        money.IsSuccess.ShouldBeFalse();
        money.Error.ShouldBe(MoneyDomainErrors.Money.InvalidMoney);
    }
    
    [Fact]
    public void Create_money_with_vnd_currency_successfully()
    {
        var money = Money.Create(500m, CurrencyType.VND);

        money.IsSuccess.ShouldBeTrue();
        money.Value!.Value.ShouldBe(500m);
        money.Value.CurrencyType.ShouldBe(CurrencyType.VND);
    }
    
    [Fact]
    public void Create_money_successfully()
    {
        var money = Money.Create(1.5m, CurrencyType.USD);

        money.IsSuccess.ShouldBeTrue();
        money.Value!.Value.ShouldBe(1.5m);
        money.Value.CurrencyType.ShouldBe(CurrencyType.USD);
    }

    [Fact]
    public void Two_money_instances_equal_if_be_the_same_value_and_currency_type()
    {
        var money1 = Money.Create(200, CurrencyType.USD).Value;
        var money2 = Money.Create(200, CurrencyType.USD).Value;

        money1.ShouldBe(money2);
        money1!.GetHashCode().ShouldBe(money2!.GetHashCode());
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_be_different_value()
    {
        var money1 = Money.Create(300m, CurrencyType.USD).Value;
        var money2 = Money.Create(200m, CurrencyType.USD).Value;

        money1.ShouldNotBe(money2);
        money1!.GetHashCode().ShouldNotBe(money2!.GetHashCode());
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_be_different_currency_type()
    {
        var money1 = Money.Create(200m, CurrencyType.USD).Value;
        var money2 = Money.Create(200m, CurrencyType.VND).Value;

        money1.ShouldNotBe(money2);
        money1!.GetHashCode().ShouldNotBe(money2!.GetHashCode());
    }
}