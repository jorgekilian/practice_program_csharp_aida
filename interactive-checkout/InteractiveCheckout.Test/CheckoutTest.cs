using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    [Test]
    public void not_accepting_terms_throw_exception()
    {
        var polkaDotSocks = new Product("Polka-dot Socks");
        ForTestCheckout.termsAndConditionsConfirmation.WasAccepted().Returns(false);
        var checkout = new ForTestCheckout(polkaDotSocks, null);

        Assert.Throws<OrderCancelledException>(() => checkout.ConfirmOrder());
    }

    [Test]
    public void accepting_terms_and_not_accepting_newsletter()
    {
        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");
        ForTestCheckout.termsAndConditionsConfirmation.WasAccepted().Returns(true);
        ForTestCheckout.newsLetterConfirmation.WasAccepted().Returns(false);
        var checkout = new ForTestCheckout(polkaDotSocks, emailService);

        checkout.ConfirmOrder();

        emailService.Received(0).SubscribeUserFor(Arg.Any<Product>());
    }

    [Test]
    public void accepting_terms_and_accepting_newsletter()
    {
        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");
        ForTestCheckout.termsAndConditionsConfirmation.WasAccepted().Returns(true);
        ForTestCheckout.newsLetterConfirmation.WasAccepted().Returns(true);
        var checkout = new ForTestCheckout(polkaDotSocks, emailService);

        checkout.ConfirmOrder();

        emailService.Received(1).SubscribeUserFor(polkaDotSocks);
    }


    public class ForTestCheckout : Checkout
    {
        private int counter = 0;
        public static UserConfirmation newsLetterConfirmation = Substitute.For<UserConfirmation>();
        public static UserConfirmation termsAndConditionsConfirmation = Substitute.For<UserConfirmation>();

        public ForTestCheckout(Product product, IEmailService emailService) : base(product, emailService)
        {
        }

        protected override UserConfirmation CreateUserConfirmation(string textMessage)
        {
            if (counter == 0)
            {
                counter++;
                return newsLetterConfirmation;
            }
            return termsAndConditionsConfirmation;
        }
    }
}