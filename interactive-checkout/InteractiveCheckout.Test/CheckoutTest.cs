using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest {
    [Test]
    public void Test() {
        // note for tester:
        // Accept Newsletter
        // Do not Accept Terms

        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");

        var checkout = new ForTestCheckout(polkaDotSocks, emailService);

        Assert.Throws<OrderCancelledException>(() => checkout.ConfirmOrder());
    }


    public class ForTestCheckout : Checkout {

        List<bool> wasAcceptedAnswers;
        private int counter = 0;

        public ForTestCheckout(Product product, IEmailService emailService) : base(product, emailService) {
            wasAcceptedAnswers = new List<bool> { true, false };
        }
        protected override IUserConfirmation CreateUserConfirmation(string textMessage) {
            var userConfirmation = Substitute.For<IUserConfirmation>();
            userConfirmation.WasAccepted().Returns(wasAcceptedAnswers[counter]);
            counter++;
            return userConfirmation;
        }
    }
}