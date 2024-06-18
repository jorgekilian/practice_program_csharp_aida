namespace InteractiveCheckout;

public class Checkout
{
    private readonly IEmailService _emailService;

    private readonly UserConfirmation _newsLetterSubscribed;
    private readonly Product _product;

    private readonly UserConfirmation _termsAndConditionsAccepted;

    public Checkout(Product product, IEmailService emailService)
    {
        _product = product;
        _emailService = emailService;
        _newsLetterSubscribed = CreateUserConfirmation("Subscribe to our product " + product + " newsletter?");
        _termsAndConditionsAccepted = CreateUserConfirmation(
            "Accept our terms and conditions?\n" + //
            "(Mandatory to place order for " + product + ")");
    }

    protected virtual UserConfirmation CreateUserConfirmation(string textMessage) {
        return new UserConfirmation(textMessage);
    }

    public void ConfirmOrder()
    {
        if (!_termsAndConditionsAccepted.WasAccepted()) throw new OrderCancelledException(_product);
        if (_newsLetterSubscribed.WasAccepted()) _emailService.SubscribeUserFor(_product);
    }
}
