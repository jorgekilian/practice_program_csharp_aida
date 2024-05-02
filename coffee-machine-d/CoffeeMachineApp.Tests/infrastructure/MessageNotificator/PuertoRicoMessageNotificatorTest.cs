namespace CoffeeMachineApp.Tests.infrastructure.MessageNotificator;

public class PuertoRicoMessageNotificatorTest : CultureBasedMessageNotificatorTest
{
    protected override string CreateLocale()
    {
        return "es-PR";
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