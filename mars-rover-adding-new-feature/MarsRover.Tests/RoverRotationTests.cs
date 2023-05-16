using Xunit;

namespace MarsRover.Tests;

public class RoverRotationTests
{
    [Fact]
    public void FacingNorthRotateLeft()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("l");

        Assert.Equal(new Rover(0, 0, "W"), rover);
    }

    [Fact]
    public void FacingNorthRotateRight()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("r");

        Assert.Equal(new Rover(0, 0, "E"), rover);
    }

    [Fact]
    public void FacingSouthRotateLeft()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("l");

        Assert.Equal(new Rover(0, 0, "E"), rover);
    }

    [Fact]
    public void FacingSouthRotateRight()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("r");

        Assert.Equal(new Rover(0, 0, "W"), rover);
    }

    [Fact]
    public void FacingWestRotateLeft()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("l");

        Assert.Equal(new Rover(0, 0, "S"), rover);
    }

    [Fact]
    public void FacingWestRotateRight()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("r");

        Assert.Equal(new Rover(0, 0, "N"), rover);
    }

    [Fact]
    public void FacingEastRotateLeft()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("l");

        Assert.Equal(new Rover(0, 0, "N"), rover);
    }

    [Fact]
    public void FacingEastRotateRight()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("r");

        Assert.Equal(new Rover(0, 0, "S"), rover);
    }
}