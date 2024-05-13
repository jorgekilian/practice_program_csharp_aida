using MarsRover.commands;
using MarsRover.communicationProtocols.commandExtractor;

namespace MarsRover.communicationProtocols;

public class CnsaCommunicationProtocol : CommunicationProtocol
{
    public CnsaCommunicationProtocol() : base(new FixedLengthCommandExtractor(2), new CnsaCommandMapper())
    {
    }
}

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