namespace LegacySecurityManager;

public class ConsoleInput : Input {
    private readonly Notifier notifier;
    private readonly InputReader inputReader;

    public ConsoleInput(Notifier notifier, InputReader inputReader) {
        this.notifier = notifier;
        this.inputReader = inputReader;
    }

    public string Request(string requestMessage)
    {
        notifier.Notify(requestMessage);
        return inputReader.Read();
    }
}