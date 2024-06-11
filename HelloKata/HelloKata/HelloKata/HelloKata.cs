namespace HelloKata;

public class HelloKata {
    private readonly Notifier notifier;
    private readonly MyHour myHour;

    public HelloKata(Notifier notifier, MyHour myHour) {
        this.notifier = notifier;
        this.myHour = myHour;
    }

    public void Hello() {

        var hour = myHour.Get();

        if (hour > 6 && hour <= 12) {
            notifier.Notify("Buenas días");
            return;
        }

        if (hour > 12 && hour < 20) {
            notifier.Notify("Buenas tardes");
            return;
        }

        notifier.Notify("Buenas noches");
    }
}