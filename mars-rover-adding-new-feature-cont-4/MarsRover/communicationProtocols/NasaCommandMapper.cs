using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class NasaCommandMapper : CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "l" => new RotationLeft(),
            "r" => new RotationRight(),
            "f" => new MovementForward(displacement),
            _ => new MovementBackward(displacement)
        };
    }
}