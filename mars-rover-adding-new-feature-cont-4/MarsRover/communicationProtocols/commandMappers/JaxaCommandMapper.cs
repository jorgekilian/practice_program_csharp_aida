using MarsRover.commands;

namespace MarsRover.communicationProtocols.commandMappers;

public class JaxaCommandMapper : CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation)
    {
        if (commandRepresentation == "at") return new MovementBackward(displacement);

        if (commandRepresentation == "iz") return new RotationLeft();

        if (commandRepresentation == "der") return new RotationRight();

        return new MovementForward(displacement);
    }
}