using System.Collections.Generic;
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
        decimal TotalB = 0;
        decimal TotalS = 0;

        if (string.IsNullOrWhiteSpace(ordersSequence)) {
            ShowSummary(TotalB, TotalS);
            return;
        }
        var transactions = ParseTransactions(ordersSequence);

        foreach (var transaction in transactions) {
            if (transaction.Action == 'B') {
                TotalB += (decimal)(transaction.Quantity * transaction.Price);
            }
            if (transaction.Action == 'S') {
                TotalS += (decimal)(transaction.Quantity * transaction.Price);
            }
        }
        ShowSummary(TotalB, TotalS);

    }
    private void ShowSummary(decimal TotalB, decimal TotalS) {
        notifier.Notify($"{dateTimeProvider.Now()} Buy: € {TotalB.ToString("0.00", new CultureInfo("en-US"))}, Sell: € {TotalS.ToString("0.00", new CultureInfo("en-US"))}");
    }

    private IEnumerable<Transaction> ParseTransactions(string input) {
        var transactions = new List<Transaction>();

        var parts = input.Split(',');

        foreach (var part in parts) {
            var elements = part.Split(' ');

            var transaction = new Transaction {
                Symbol = elements[0],
                Quantity = int.Parse(elements[1]),
                Price = double.Parse(elements[2], CultureInfo.InvariantCulture),
                Action = char.Parse(elements[3])
            };

            transactions.Add(transaction);
        }

        return transactions;
    }


}

public record Transaction {
    public string Symbol { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public char Action { get; set; }

}