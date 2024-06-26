using MarsRover.commands;

namespace MarsRover.communicationProtocols.commandMappers;

public class CnsaCommandMapper : CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "bx" => new MovementForward(displacement),
            "tf" => new MovementBackward(displacement),
            "ah" => new RotationLeft(),
            _ => new RotationRight()
        };
    }
}