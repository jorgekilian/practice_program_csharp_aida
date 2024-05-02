namespace CoffeeMachineApp.Tests.infrastructure.MessageNotificator;

public class BritainMessageNotificatorTest : CultureBasedMessageNotificatorTest
{
    protected override string CreateLocale()
    {
        return "en-GB";
    }

    protected override string MissingMoneyMessage(decimal missingPriceAmount)
    {
        return $"You missing {missingPriceAmount.ToString(MessageCulture)}";
    }

    protected override string SelectDrinkMessage()
    {
        return "Please, select a drink!";
    }

}