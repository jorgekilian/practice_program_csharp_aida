using NSubstitute;
using NUnit.Framework;

namespace StockBroker.Tests
{
    public class StockBrokerServiceTest
    {
        //Tests
        // Una orden de tipo B
        // Una orden de tipo S
        // Dos ordenes de tipo B
        // Dos ordenes de tipo S
        // Dos ordenes, una de tipo B y otra de tipo S
        // Tres ordenes, dos de tipo B y una de tipo S
        // Tres ordenes, una de tipo B y dos de tipo S
        // Cuatro ordenes, dos de tipo B y dos de tipo S
        // Una orden que fallará
        // Dos ordenes y una fallará
        // Tres ordenes y dos fallaran

        //Colaboradores
        // Notifier
        // Servicio Broker Online
        // DatetimeProvider

        DateTimeProvider _dateTimeProvider;
        StockBrokerService _stockBrokerService;
        Notifier _notifier;


        [Test]
        public void process_one_type_order_B()
        {
            _notifier = Substitute.For<Notifier>();
            _stockBrokerService = Substitute.For<StockBrokerService>();
            _dateTimeProvider = Substitute.For<DateTimeProvider>();
            var stockBrokerClient = new StockBrokerClient(_notifier, _stockBrokerService, _dateTimeProvider);
            
            stockBrokerClient.PlaceOrders("GOOG 300 829.08 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 248724.00, Sell: € 0.00");
        }


    }
}