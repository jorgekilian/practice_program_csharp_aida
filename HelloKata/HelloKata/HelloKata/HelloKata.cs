namespace HelloKata;

public class HelloKata {
    private readonly Notifier notifier;

    public HelloKata(Notifier notifier) {
        this.notifier = notifier;
    }

    public void Hello() {
        notifier.Notify("Buenas noches");
    }
}