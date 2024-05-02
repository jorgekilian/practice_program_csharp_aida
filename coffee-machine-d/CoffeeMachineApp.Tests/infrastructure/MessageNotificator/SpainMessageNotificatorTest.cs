namespace CoffeeMachineApp.Tests.infrastructure.MessageNotificator;

public class SpainMessageNotificatorTest : CultureBasedMessageNotificatorTest
{
    protected override string CreateLocale()
    {
        return "es-ES";
    }

    protected override string MissingMoneyMessage(decimal missingPriceAmount)
    {
        return $"Te faltan {missingPriceAmount.ToString(MessageCulture)}";
    }

    protected override string SelectDrinkMessage()
    {
        return "Por favor, ¡selecciona una bebida!";
    }

}