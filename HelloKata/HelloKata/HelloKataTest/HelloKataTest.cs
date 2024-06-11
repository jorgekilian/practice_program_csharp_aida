using NSubstitute;
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

        [Test]
        public void greet_with_good_night() {

            _notifier = Substitute.For<Notifier>();
            var helloKata = new HelloKata(_notifier);

            helloKata.Hello();

            _notifier.Received(1).Notify("Buenas noches");

        }

    }
}
