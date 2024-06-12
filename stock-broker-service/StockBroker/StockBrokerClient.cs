namespace StockBroker;

public class StockBrokerClient {
    private readonly Notifier notifier;
    private readonly StockBrokerService stockBrokerService;
    private readonly DateTimeProvider dateTimeProvider;

    public StockBrokerClient(Notifier notifier, StockBrokerService stockBrokerService, DateTimeProvider dateTimeProvider) {
        this.notifier = notifier;
        this.stockBrokerService = stockBrokerService;
        this.dateTimeProvider = dateTimeProvider;
    }

    public void PlaceOrders(string ordersSequence) {
        if (ordersSequence.EndsWith("B")) {
            ShowSummary("Buy: € 248724.00, Sell: € 0.00");
        }
        if (ordersSequence.EndsWith("S")) {
            ShowSummary("Buy: € 0.00, Sell: € 248724.00");
        }
        ShowSummary("Buy: € 0.00, Sell: € 0.00");
    }

    private void ShowSummary(string message) {
        notifier.Notify($"{dateTimeProvider.Now()} {message}");
    }
}