namespace Checkout;

public class Receipt
{
    public Receipt(Money amount, Money tax, Money total)
    {
        Amount = amount;
        Tax = tax;
        Total = total;
    }

    public Money Amount { get; }

    public Money Tax { get; }

    public Money Total { get; }

    public IEnumerable<string> Format()
    {
        return new List<string>
        {
            //
            "Receipt", //
            "=======", //
            "Item 1 ... " + Amount.Format(), //
            "Tax    ... " + Tax.Format(), //
            "----------------", //
            "Total  ... " + Total.Format() //
        };
    }

    protected bool Equals(Receipt other) {
        return Amount.Equals(other.Amount) && Tax.Equals(other.Tax) && Total.Equals(other.Total);
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Receipt)obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Amount, Tax, Total);
    }

    public override string ToString() {
        return $"{nameof(Amount)}: {Amount}, {nameof(Tax)}: {Tax}, {nameof(Total)}: {Total}";
    }
}