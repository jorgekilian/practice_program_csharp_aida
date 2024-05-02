using System.Globalization;
using CoffeeMachineApp.core;
using CoffeeMachineApp.infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachineApp.Tests.infrastructure.MessageNotificator;

public abstract class CultureBasedMessageNotificatorTest
{
    protected CultureInfo MessageCulture;
    private DrinkMakerDriver _drinkMakerDriver;
    private CultureBasedMessageNotificator _messageNotificator;

    [SetUp]
    public void SetUp()
    {
        MessageCulture = new CultureInfo(CreateLocale());
        _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
        _messageNotificator = new CultureBasedMessageNotificator(MessageCulture, _drinkMakerDriver);
    }

    [Test]
    public void Send_Select_Drink()
    {
        var expectedMessage = Message.Create(SelectDrinkMessage());

        _messageNotificator.NotifySelectDrink();

        _drinkMakerDriver.Received(1).Notify(expectedMessage);
    }

    [Test]
    public void Send_Missing_Price()
    {
        var givenMissingPrice = 0.5m;
        var expectedMessage = Message.Create(MissingMoneyMessage(givenMissingPrice));

        _messageNotificator.NotifyMissingPrice(givenMissingPrice);

        _drinkMakerDriver.Received(1).Notify(expectedMessage);
    }

    protected abstract string CreateLocale();
    protected abstract string MissingMoneyMessage(decimal missingPriceAmount);
    protected abstract string SelectDrinkMessage();

}