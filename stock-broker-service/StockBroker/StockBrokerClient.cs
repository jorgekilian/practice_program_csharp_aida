using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Transactions;

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
        if (IsEmptyOrdersSequence(ordersSequence)) {
            ShowSummary(0, 0);
            return;
        }
        var transactions = Transactions.ParseTransactions(ordersSequence);

        foreach (var transaction in transactions.AllTransactions) {
            stockBrokerService.Process(transaction);
        }
        
        ShowSummary(transactions.CalculateBuy(), transactions.CalculateSell());

    }

    private static bool IsEmptyOrdersSequence(string ordersSequence) {
        return string.IsNullOrWhiteSpace(ordersSequence);
    }

    private void ShowSummary(decimal TotalB, decimal TotalS) {
        notifier.Notify($"{dateTimeProvider.Now()} Buy: € {TotalB.ToString("0.00", new CultureInfo("en-US"))}, Sell: € {TotalS.ToString("0.00", new CultureInfo("en-US"))}");
    }
}