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

        if (IsMorning(hour)) {
            Greet("Buenas días");
            return;
        }

        if (IsAfternoon(hour)) {
            Greet("Buenas tardes");
            return;
        }

        Greet("Buenas noches");
    }

    private static bool IsAfternoon(decimal hour) {
        return hour > 12 && hour <= 20;
    }

    private static bool IsMorning(decimal hour) {
        return hour > 6 && hour <= 12;
    }

    private void Greet(string message) {
        notifier.Notify(message);
    }
}