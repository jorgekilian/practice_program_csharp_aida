using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest
{
    [Test]
    public void checkout_create_and_store_receipt()
    {
        var checkout = new ForTestingCheckout();

        var receipt = checkout.CreateReceipt(new Money(12));
        var expectedReceived = new Receipt(new Money(12m), new Money(2.4m), new Money(14.4m));

        Assert.That(checkout.storedReceipts.First(), Is.EqualTo(expectedReceived));
        Assert.That(receipt, Is.EqualTo(expectedReceived));

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