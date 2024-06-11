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

        if (hour > 6 && hour < 12) {
            notifier.Notify("Buenas días");
            return;
        }

        notifier.Notify("Buenas noches");
    }
}