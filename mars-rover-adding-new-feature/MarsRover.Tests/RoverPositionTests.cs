using Xunit;

namespace MarsRover.Tests;

public class RoverPositionTests
{
    [Fact]
    public void FacingNorthMoveForward()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("f");

        Assert.Equal(new Rover(0, 1, "N"), rover);
    }

    [Fact]
    public void FacingNorthMoveBackward()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("b");

        Assert.Equal(new Rover(0, -1, "N"), rover);
    }

    [Fact]
    public void FacingSouthMoveForward()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("f");

        Assert.Equal(new Rover(0, -1, "S"), rover);
    }

    [Fact]
    public void FacingSouthMoveBackward()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("b");

        Assert.Equal(new Rover(0, 1, "S"), rover);
    }

    [Fact]
    public void FacingWestMoveForward()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("f");

        Assert.Equal(new Rover(-1, 0, "W"), rover);
    }

    [Fact]
    public void FacingWestMoveBackward()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("b");

        Assert.Equal(new Rover(1, 0, "W"), rover);
    }

    [Fact]
    public void FacingEastMoveForward()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("f");

        Assert.Equal(new Rover(1, 0, "E"), rover);
    }

    [Fact]
    public void FacingEastMoveBackward()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("b");

        Assert.Equal(new Rover(-1, 0, "E"), rover);
    }
}