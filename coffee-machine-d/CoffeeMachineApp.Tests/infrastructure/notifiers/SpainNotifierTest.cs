﻿using System.Globalization;
using CoffeeMachineApp.core;
using CoffeeMachineApp.infrastructure.notifiers;
using NSubstitute;

namespace CoffeeMachineApp.Tests.infrastructure.notifiers;

public class SpainNotifierTest : NotifierTest
{
    private const string CultureInfoName = "es-Es";

    protected override Notifier GetNotifier(DrinkMakerDriver drinkMakerDriver)
    {
        return new CultureBasedNotifier(GetCultureInfo(CultureInfoName), drinkMakerDriver);
    }

    protected override void CheckMissingMoneyMessage(DrinkMakerDriver drinkMakerDriver, decimal missingPriceAmount)
    {
        drinkMakerDriver.Received(1).Notify(
            Message.Create($"Te faltan {missingPriceAmount.ToString(GetCultureInfo(CultureInfoName))}")
        );
    }

    protected override void CheckSelectDrinkMessage(DrinkMakerDriver drinkMakerDriver)
    {
        drinkMakerDriver.Received(1).Notify(Message.Create("Por favor, ¡selecciona una bebida!"));
    }
}