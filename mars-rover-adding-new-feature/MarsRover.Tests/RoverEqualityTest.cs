using Xunit;

namespace MarsRover.Tests
{
    public class RoverEqualityTest
    {
        [Fact]
        public void EqualRovers()
        {
            Assert.Equal(new Rover(1, 1, "N"), new Rover(1, 1, "N"));
        }

        [Fact]
        public void NotEqualRovers()
        {
            Assert.NotEqual(new Rover(1, 1, "N"), new Rover(1, 1, "S"));
            Assert.NotEqual(new Rover(1, 1, "N"), new Rover(1, 2, "N"));
            Assert.NotEqual(new Rover(1, 1, "N"), new Rover(0, 1, "N"));
        }
    }
}