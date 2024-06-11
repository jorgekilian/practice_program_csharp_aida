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

        [Test]
        public void greet_with_good_morning() {
            _myHour.Get().Returns(8);

            var helloKata = new HelloKata(_notifier, _myHour);
            

            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas días");
        }

        [Test]
        public void greet_with_good_afternoo() {
            _myHour.Get().Returns(18);

            var helloKata = new HelloKata(_notifier, _myHour);


            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas tardes");
        }
    }
}
