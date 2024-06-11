using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace HelloKata.Test {


    public class HelloKataTest {

        // Lista de Tests
        // Devolver "buenas noches"
        // Devolver "buenos días" si la hora es entre las 6:00 AM y las 12:00AM
        // Devolver "buenas tardes" si la hora es entre las 12:00 y las 8:00PM

        // Colaboradores
        // Notifier
        // ¿Uso horario?
        private Notifier _notifier;
        private MyHour _myHour;

        [SetUp]
        public void SetUp() {
            _notifier = Substitute.For<Notifier>();
            _myHour = Substitute.For<MyHour>();
        }

        [Test]
        public void greet_with_good_night() {
            var helloKata = new HelloKata(_notifier, _myHour);

            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas noches");

        }

        [TestCase(7)]
        [TestCase(12)]
        public void greet_with_good_morning(int hour) {
            _myHour.Get().Returns(hour);

            var helloKata = new HelloKata(_notifier, _myHour);
            

            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas días");
        }

        [TestCase(13)]
        [TestCase(20)]
        public void greet_with_good_afternoo(int hour) {
            _myHour.Get().Returns(hour);

            var helloKata = new HelloKata(_notifier, _myHour);


            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas tardes");
        }
    }
}
