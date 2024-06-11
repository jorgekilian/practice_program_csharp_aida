using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace HelloKata.Test {


    public class HelloKataTest {

        private Notifier _notifier;
        private Clock _clock;
        private HelloKata _helloKata;

        [SetUp]
        public void SetUp() {
            _notifier = Substitute.For<Notifier>();
            _clock = Substitute.For<Clock>();
            _helloKata = new HelloKata(_notifier, _clock);
        }

        [TestCase(20.01)]
        [TestCase(5.59)]
        public void greet_with_good_night_during_the_night(decimal hour) {
            _clock.GetHour().Returns(hour);
            
            _helloKata.Hello();

            _notifier.Received(1).Notify("Buenas noches");

        }

        [TestCase(6.01)]
        [TestCase(7)]
        [TestCase(12)]
        public void greet_with_good_morning_during_the_morning(decimal hour) {
            _clock.GetHour().Returns(hour);

            _helloKata.Hello();

            _notifier.Received(1).Notify("Buenas días");
        }

        [TestCase(12.01)]
        [TestCase(13)]
        [TestCase(20)]
        public void greet_with_good_afternoon_during_the_afternoon(decimal hour) {
            _clock.GetHour().Returns(hour);

            _helloKata.Hello();

            _notifier.Received(1).Notify("Buenas tardes");
        }
    }
}
