using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockBroker;

public class Transactions {
    private List<Transaction> _transactions;

    public IEnumerable<Transaction> AllTransactions => _transactions;

    private Transactions() {
        _transactions = new List<Transaction>();
    }

    public static Transactions ParseTransactions(string input) {
        Transactions transactions = new Transactions();

        if (string.IsNullOrWhiteSpace(input)) {
            return transactions;
        }

        var parts = input.Split(',');

        foreach (var part in parts) {
            var elements = part.Split(' ');

            var transaction = new Transaction {
                Symbol = elements[0],
                Quantity = int.Parse(elements[1]),
                Price = double.Parse(elements[2], CultureInfo.InvariantCulture),
                Action = char.Parse(elements[3])
            };

            transactions._transactions.Add(transaction);
        }

        return transactions;
    }

    public decimal CalculateBuy() {
        return _transactions.Where(x => x.IsBuyTransaction() && !x.Failed).Sum(x => x.CalculateTotal());
    }


    public decimal CalculateSell() {
        return _transactions.Where(x => x.IsSellTransaction() && !x.Failed).Sum(x => x.CalculateTotal());
    }

    public IEnumerable<Transaction> GetFailedTransactions() {
        return _transactions.Where(x => x.Failed);
    }
}