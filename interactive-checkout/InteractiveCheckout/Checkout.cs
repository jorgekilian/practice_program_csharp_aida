namespace InteractiveCheckout;

public class Checkout
{
    private readonly IEmailService _emailService;

    private readonly IUserConfirmation _newsLetterSubscribed;
    private readonly Product _product;

    private readonly IUserConfirmation _termsAndConditionsAccepted;

    public Checkout(Product product, IEmailService emailService)
    {
        _product = product;
        _emailService = emailService;
        _newsLetterSubscribed = CreateUserConfirmation("Subscribe to our product " + product + " newsletter?");
        _termsAndConditionsAccepted = CreateUserConfirmation(
            "Accept our terms and conditions?\n" + //
            "(Mandatory to place order for " + product + ")");
    }

    protected virtual IUserConfirmation CreateUserConfirmation(string textMessage) {
        return new UserConfirmation(textMessage);
    }

    public virtual void ConfirmOrder()
    {
        if (!_termsAndConditionsAccepted.WasAccepted()) throw new OrderCancelledException(_product);
        if (_newsLetterSubscribed.WasAccepted()) _emailService.SubscribeUserFor(_product);
    }
}