namespace StockBroker;

public interface StockBrokerService {
    void Process(Transaction order);
}