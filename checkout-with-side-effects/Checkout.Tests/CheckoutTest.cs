using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest {
    [Test]
    public void checkout_create_and_store_receipt() {
        var checkout = new ForTestingCheckout();

        var receipt = checkout.CreateReceipt(new Money(12));
        var expectedReceived = new Receipt(new Money(12m), new Money(2.4m), new Money(14.4m));

        Assert.That(checkout.storedReceipts.First(), Is.EqualTo(expectedReceived));
        Assert.That(checkout.storedReceipts.Count, Is.EqualTo(1));
        Assert.That(receipt, Is.EqualTo(expectedReceived));

    }

    [Test]
    public void checkout_create_and_store_two_receipts() {
        var checkout = new ForTestingCheckout();
        var expectedReceived = new List<Receipt>();


        var receipt = checkout.CreateReceipt(new Money(12));
        var otherReceipt = checkout.CreateReceipt(new Money(100));
        expectedReceived.Add(new Receipt(new Money(12m), new Money(2.4m), new Money(14.4m)));
        expectedReceived.Add(new Receipt(new Money(100m), new Money(20m), new Money(120m)));

        Assert.That(checkout.storedReceipts, Is.EquivalentTo(expectedReceived));
        Assert.That(receipt, Is.EqualTo(expectedReceived[0]));
        Assert.That(otherReceipt, Is.EqualTo(expectedReceived[1]));
        Assert.That(checkout.storedReceipts.Count, Is.EqualTo(2));
    }

    public class ForTestingCheckout : Checkout {
        public List<Receipt> storedReceipts;

        public ForTestingCheckout() {
            storedReceipts = new List<Receipt>();
        }

        protected override void StoreReceipt(Receipt receipt) {
            storedReceipts.Add(receipt);
        }
    }
}