using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
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
        var transactions = Transactions.ParseTransactions(ordersSequence);

        foreach (var transaction in transactions.AllTransactions) {
            try {
                stockBrokerService.Process(transaction);
            }
            catch (Exception e) {
                transaction.HasFailed();
            }
            
        }
        
        ShowSummary(transactions);

    }

    private void ShowSummary(Transactions transactions) {
        var TotalBuy = transactions.CalculateBuy().ToString("0.00", new CultureInfo("en-US"));
        var TotalSell = transactions.CalculateSell().ToString("0.00", new CultureInfo("en-US"));
        var FailedTransactions = GetFailedMessage(transactions.GetFailedTransactions());

        notifier.Notify($"{dateTimeProvider.Now()} Buy: € {TotalBuy}, Sell: € {TotalSell}{FailedTransactions}");
    }

    private string GetFailedMessage(IEnumerable<Transaction> failedTransactions) {
        if (!failedTransactions.Any()) return string.Empty;

        return $", Failed: {string.Join(", ",failedTransactions.Select(x => x.Symbol))}";
    }
}