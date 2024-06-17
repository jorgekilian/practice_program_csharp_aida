using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockBroker;

public class TransactionSummaryMessageFormatter {
    private readonly DateTimeProvider dateTimeProvider;
    public TransactionSummaryMessageFormatter(DateTimeProvider dateTimeProvider) {
        this.dateTimeProvider = dateTimeProvider;
    }

    public string CreateSummary(Transactions transactions) {
        var TotalBuy = transactions.CalculateBuy().ToString("0.00", new CultureInfo("en-US"));
        var TotalSell = transactions.CalculateSell().ToString("0.00", new CultureInfo("en-US"));
        var FailedTransactions = CreateFailedMessage(transactions.GetFailedTransactions());

        return $"{this.dateTimeProvider.Now()} Buy: € {TotalBuy}, Sell: € {TotalSell}{FailedTransactions}";
    }

    private string CreateFailedMessage(IEnumerable<Transaction> failedTransactions) {
        if (!failedTransactions.Any()) return string.Empty;

        return $", Failed: {string.Join(", ", failedTransactions.Select(x => x.Symbol))}";
    }
}