using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests {
    public class RoverUsingJointCommunicationProtocol {

        [Test]
        public void empty_sequence() {
            
            var rover = JointRover().Build();

            rover.Receive("");

            Assert.That(rover, Is.EqualTo(JointRover().Build()));
        }

        [Test]
        public void Forward_Commands_Using_Esa_Protocol() {
            var rover = JointRover().WithCoordinates(1,1).Facing("N").Build();

            rover.Receive("*b");

            Assert.That(rover, Is.EqualTo(JointRover().WithCoordinates(1, 2).Facing("N").Build()));

        }

        [Test]
        public void Command_Sequence_Using_Cnsa_Protocol() {
            var rover = JointRover().Facing("N").Build();

            rover.Receive("%pl");

            Assert.That(rover, Is.EqualTo(JointRover().Facing("E").Build()));

        }

    }
}
