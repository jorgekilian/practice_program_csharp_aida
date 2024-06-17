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
        // * Una orden que fallará
        // Dos ordenes y una fallará
        // * Dos ordenes y dos fallaran
        // 

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
        public void process_one_type_order_buy() {

            stockBrokerClient.PlaceOrders("GOOG 300 829.08 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 248724.00, Sell: € 0.00");
        }

        [Test]
        public void process_one_type_order_sell() {

            stockBrokerClient.PlaceOrders("GOOG 300 829.08 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 248724.00");
        }

        [Test]
        public void process_two_types_orders_buy() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 10603.00, Sell: € 0.00");
        }

        [Test]
        public void process_two_types_orders_sell() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 S,AAPL 50 139.78 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 10603.00");
        }

        [Test]
        public void process_two_orders_one_type_orders_sell_and_other_type_order_buy() {

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 3614.00, Sell: € 6989.00");
        }

        [Test]
        public void send_the_orders_to_service_broker() {
            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S");

            _stockBrokerService.Received(2).Process(Arg.Any<Transaction>());
        }

        [Test]
        public void send_one_order_and_fails() {
            _stockBrokerService.When(x => x.Process(Arg.Any<Transaction>())).Throw(new Exception("Transaction could not be processed"));

            stockBrokerClient.PlaceOrders("GOOG 300 829.08 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 0.00, Failed: GOOG");
        }

        [Test]
        public void send_two_orders_and_two_fails() {
            _stockBrokerService.When(x => x.Process(Arg.Any<Transaction>())).Throw(new Exception("Transaction could not be processed"));

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 0.00, Sell: € 0.00, Failed: ZNGA, AAPL");
        }

        [Test]
        public void send_three_orders_and_one_fails() {
            _stockBrokerService.When(x => x.Process(Arg.Is<Transaction>(x => x.Symbol.Equals("AAPL")))).Throw(new Exception("Transaction could not be processed"));
            _stockBrokerService.When(x => x.Process(Arg.Is<Transaction>(x => x.Symbol.Equals("NEW")))).Throw(new Exception("Transaction could not be processed"));

            stockBrokerClient.PlaceOrders("ZNGA 1300 2.78 B,AAPL 50 139.78 S,GOOG 300 829.08 S,RAMON 1 1500 B,KILIAN 2 100 S,NEW 3 33 B");

            _notifier.Received(1).Notify("12/20/2023 1:45 AM Buy: € 5114.00, Sell: € 248924.00, Failed: AAPL, NEW");
        }
    }


}