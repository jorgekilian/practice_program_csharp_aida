namespace StockBroker;

public record Transaction {
    public string Symbol { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public char Action { get; set; }

}