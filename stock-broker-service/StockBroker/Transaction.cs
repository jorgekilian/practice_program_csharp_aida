namespace StockBroker;

public record Transaction {
    public string Symbol { get; init; }
    public int Quantity { get; init; }
    public double Price { get; init; }
    public char Action { get; init; }

    public decimal CalculateTotal() {
        return (decimal)(Quantity * Price);
    }

    public bool IsBuyTransaction() {
        return Action.Equals('B');
    }

    public bool IsSellTransaction() {
        return Action.Equals('S');
    }
}