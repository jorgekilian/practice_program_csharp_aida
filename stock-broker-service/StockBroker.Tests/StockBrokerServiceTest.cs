using NSubstitute;
using NUnit.Framework;

namespace StockBroker.Tests {
    public class StockBrokerServiceTest {
        //Tests
        // * Orden vacía
        // * Una orden de tipo B
        // * Una orden de tipo S
        // * Dos ordenes de tipo B
        // * Dos ordenes de tipo S
        // * Dos ordenes, una de tipo B y otra de tipo S
        // -- Tres ordenes, dos de tipo B y una de tipo S
        // -- Tres ordenes, una de tipo B y dos de tipo S
        // -- Cuatro ordenes, dos de tipo B y dos de tipo S
        // * Confirmar que se llama al servicio 
        // Una orden que fallará
        // Dos ordenes y una fallará
        // Tres ordenes y dos fallaran

        //Colaboradores
        // Notifier (Command)
        // Servicio Broker Online (Command)
        // DatetimeProvider (Query)

        DateTimeProvider _dateTimeProvider;
        StockBrokerService _stockBrokerService;
        Notifier _notifier;
        private StockBrokerClient stockBrokerClient;

        [SetUp]
        public void Setup() {
            _notifier = Substitute.For<Notifier>();
            _stockBrokerService = Substitute.For<StockBrokerService>();
            _dateTimeProvider = Substitute.For<DateTimeProvider>();
            _dateTimeProvider.Now().Returns("12/20/2023 1:45 AM");
            stockBrokerClient = new StockBrokerClient(_notifier, _stockBrokerService, _dateTimeProvider);
        }

        [Test]
        public void process_empty_order() {

            stockBrokerClient.PlaceOrders("");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 0.00");
        }
        [Test]
        public void process_one_type_order_B() {

            stockBrokerClient.PlaceOrders("GOOG 300 829.08 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 248724.00, Sell: € 0.00");
        }

        [Test]
        public void process_one_type_order_S() {

            stockBrokerClient.PlaceOrders("GOOG 300 829.08 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 248724.00");
        }

        [Test]
        public void process_two_types_orders_B() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 10603.00, Sell: € 0.00");
        }

        [Test]
        public void process_two_types_orders_S() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 S,AAPL 50 139.78 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 10603.00");
        }

        [Test]
        public void process_two_orders_one_type_orders_S_and_other_type_order_B() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 3614.00, Sell: € 6989.00");
        }

        [Test]
        public void send_the_orders_to_service_broker() {
            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S");

            _stockBrokerService.Received(2).Process(Arg.Any<Transaction>());
        }
    }


}