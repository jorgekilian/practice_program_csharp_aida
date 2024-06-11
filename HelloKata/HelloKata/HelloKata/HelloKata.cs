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
        var message = "Buenas noches";

        if (IsMorning(hour)) {
            message = "Buenos días";
        }

        if (IsAfternoon(hour)) {
            message = "Buenas tardes";
        }

        Greet(message);
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