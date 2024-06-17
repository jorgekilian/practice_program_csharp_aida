using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Transactions;

namespace StockBroker;

public class StockBrokerClient {
    private readonly Notifier notifier;
    private readonly StockBrokerService stockBrokerService;
    private readonly DateTimeProvider dateTimeProvider;
    private TransactionSummaryMessageFormatter transactionSummary;

    public StockBrokerClient(Notifier notifier, StockBrokerService stockBrokerService, DateTimeProvider dateTimeProvider) {
        this.notifier = notifier;
        this.stockBrokerService = stockBrokerService;
        this.dateTimeProvider = dateTimeProvider;
        transactionSummary = new TransactionSummaryMessageFormatter(dateTimeProvider);
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
        
        notifier.Notify(transactionSummary.CreateSummary(transactions));
    }
}