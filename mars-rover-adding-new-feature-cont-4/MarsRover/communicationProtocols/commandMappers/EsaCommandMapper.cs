using MarsRover.commands;

namespace MarsRover.communicationProtocols.commandMappers;

public class EsaCommandMapper : CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "b" => new MovementForward(displacement),
            "x" => new MovementBackward(displacement),
            "f" => new RotationLeft(),
            _ => new RotationRight()
        };
    }
}