namespace HelloKata;

public class HelloKata {
    private readonly Notifier notifier;
    private readonly Clock clock;

    public HelloKata(Notifier notifier, Clock clock) {
        this.notifier = notifier;
        this.clock = clock;
    }

    public void Hello() {

        var hour = clock.GetHour();

        if (hour > 6 && hour <= 12) {
            notifier.Notify("Buenas días");
            return;
        }

        if (hour > 12 && hour <= 20) {
            notifier.Notify("Buenas tardes");
            return;
        }

        notifier.Notify("Buenas noches");
    }
}